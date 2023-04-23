using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZWOptical.EAFSDK;
using static ZWOptical.EAFSDK.EAFdll;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.IO;

// using code from here https://stackoverflow.com/questions/683330/how-to-make-a-window-always-stay-on-top-in-net

namespace ZWO_EAF_Tool
{
    

    public partial class MainForm : Form
    {
        const string settingsDirectory = "ZWOEAFTool";
        const string settingsFileName = "ZWOEAFTool.json";

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private void addMenusFromSettings()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string file = folder + "\\" + settingsDirectory + "\\" + settingsFileName;

            if (File.Exists(file))
            {
                string jsonSettings = File.ReadAllText(file);

                List<Bookmark> bookmarks = new List<Bookmark>();

                bookmarks = JsonSerializer.Deserialize<List<Bookmark>>(jsonSettings);

                foreach(Bookmark b in bookmarks)
                {
                    ToolStripMenuItem t = new ToolStripMenuItem();

                    t.Tag = b;
                    t.Text = b.name;

                    cntxtmnuBookmarks.Items.Add(t);
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            // FormBorderStyle = FormBorderStyle.FixedSingle;
            // TopMost = true;

            addMenusFromSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int iEAFCount;
            EAFdll.EAF_ERROR_CODE rc;
            EAFdll.EAF_INFO eafinfo;
            int id;

            int i;

            iEAFCount = EAFdll.GetNum();

            if (iEAFCount != 0)
            {
                for (i = 0; i < iEAFCount; i++)
                {
                    rc = EAFdll.GetID(i, out id);

                    if (rc == EAF_ERROR_CODE.EAF_SUCCESS)
                    {

                        rc = EAFdll.GetProperty(id, out eafinfo);

                        if (rc == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            EAFComboBoxItem item = new EAFComboBoxItem(eafinfo);

                            comboBoxFocuser.Items.Add(item);
                        }
                        else
                        {
                            MessageBox.Show("GetProperty() returned " + rc.ToString(), "ZWO EAF Tool");
                        }
                    }
                    else
                    {
                        MessageBox.Show("GetID() returned " + rc.ToString(), "ZWO EAF Tool");
                    }
                }
            }
            else
            {
                MessageBox.Show("no EAF Focusers found", "ZWO EAF Tool");
            }
        }

        private void comboBoxFocuser_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;
            int pos;
            
            EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

            btnMoveTo.Enabled = true;
            btnSetPosition.Enabled = true;

            eafRC = EAFdll.Open(item.id);

            if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.GetPostion(item.id, out pos);

                if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    txtTargetFocuserPosition.Text = pos.ToString();
                    lblFocuserPosition.Text = pos.ToString();
                }
                else
                {
                    MessageBox.Show("GetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }

                eafRC = EAFdll.Close(item.id);

                if(eafRC != EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
            }
        }

        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;

            if(comboBoxFocuser.SelectedItem != null)
            {
                EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                eafRC = EAFdll.Open(item.id);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    int iNewPos;

                    if(Int32.TryParse(txtTargetFocuserPosition.Text, out iNewPos))
                    {
                        eafRC = EAFdll.ResetPostion(item.id, iNewPos);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            MessageBox.Show("New Position set!", "ZWO EAF Tool");
                            lblFocuserPosition.Text = iNewPos.ToString();
                        }
                        else
                        {
                            MessageBox.Show("ResetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                        }
                    }
                    else
                    {
                        MessageBox.Show("text '" + eafRC.ToString() + "' couldn't be parsed to an integer", "ZWO EAF Tool");
                    }

                    eafRC = EAFdll.Close(item.id);

                    if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                    {
                        // everything worked
                    }
                    else
                    {
                        MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                    }
                }
                else
                {
                    MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("No focuser selected", "ZWO EAF Tool");
            }
        }

        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;

            if (comboBoxFocuser.SelectedItem != null)
            {
                EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                eafRC = EAFdll.Open(item.id);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    int iNewPos;

                    if (Int32.TryParse(txtTargetFocuserPosition.Text, out iNewPos))
                    {
                        eafRC = EAFdll.Move(item.id, iNewPos);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            comboBoxFocuser.Enabled = false;

                            tmrUpdateDisplay.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("ResetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                        }
                    }
                    else
                    {
                        MessageBox.Show("text '" + eafRC.ToString() + "' couldn't be parsed to an integer", "ZWO EAF Tool");
                    }

                    eafRC = EAFdll.Close(item.id);

                    if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                    {
                        // everything worked
                    }
                    else
                    {
                        MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                    }

                }
                else
                {
                    MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("No focuser selected", "ZWO EAF Tool");
            }

        }

        private void UpdateDisplay(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;
            int pos;
            bool bSet;
            bool bHC;

            EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

            eafRC = EAFdll.Open(item.id);

            if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.IsMoving(item.id, out bSet, out bHC);

                if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    if(bSet || bHC)
                    {
                        lblFocuserMoving.Text = "Moving";
                    }
                    else
                    {
                        lblFocuserMoving.Text = "Stopped";

                        tmrUpdateDisplay.Enabled = false;
                        comboBoxFocuser.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("IsMoving() failed, rc = " + eafRC.ToString(), "ZWO EAF Tool");
                }

                eafRC = EAFdll.GetPostion(item.id, out pos);

                if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    lblFocuserPosition.Text = pos.ToString();
                }
                else
                {
                    MessageBox.Show("GetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }

                eafRC = EAFdll.Close(item.id);

                if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    // everything worked
                }
                else
                { 
                    MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
            }

        }

        private void WaitforMove(int id)
        {
            EAFdll.EAF_ERROR_CODE eafRC;
            bool bIsMoving;
            bool bIsHCMoving;

            do
            {
                eafRC = EAFdll.IsMoving(id, out bIsMoving, out bIsHCMoving);

                if(eafRC != EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    // TODO: thown an exception here instead

                    MessageBox.Show("IsMoving() returned " + eafRC.ToString());
                    break;
                }
                else
                {
                    if(bIsMoving && bIsHCMoving)
                    {
                        Thread.Sleep(200);
                    }
                }
            } while(bIsMoving || bIsHCMoving);
        }

        private void chkStayOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStayOnTop.Checked)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;

            int iStep;

            if (!Int32.TryParse(txtBoxStep.Text, out iStep))
            {
                MessageBox.Show("Enter a valid step value");
            }
            else
            {
                if (comboBoxFocuser.SelectedItem != null)
                {
                    EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                    eafRC = EAFdll.Open(item.id);

                    if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                    {
                        int currPos;

                        eafRC = EAFdll.GetPostion(item.id, out currPos);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            eafRC = EAFdll.Move(item.id, currPos - iStep);

                            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                            {
                                lblFocuserMoving.Text = "Moving";

                                comboBoxFocuser.Enabled = false;

                                tmrUpdateDisplay.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("Move() returned" + eafRC.ToString(), "ZWO EAF Tool");
                            }
                        }
                        else
                        {
                            MessageBox.Show("GetPostion() returned" + eafRC.ToString(), "ZWO EAF Tool");

                        }

                        eafRC = EAFdll.Close(item.id);

                        if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            // everything worked
                        }
                        else
                        {
                            MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
                    }
                }
                else
                {
                    MessageBox.Show("No focuser selected", "ZWO EAF Tool");
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            EAFdll.EAF_ERROR_CODE eafRC;
            int iStep;

            if (!Int32.TryParse(txtBoxStep.Text, out iStep))
            {
                MessageBox.Show("Enter a valid step value");
            }
            else
            {
                if (comboBoxFocuser.SelectedItem != null)
                {
                    EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                    eafRC = EAFdll.Open(item.id);

                    if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                    {
                        int currPos;

                        eafRC = EAFdll.GetPostion(item.id, out currPos);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            eafRC = EAFdll.Move(item.id, currPos + iStep);

                            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                            {
                                lblFocuserMoving.Text = "Moving";

                                comboBoxFocuser.Enabled = false;

                                tmrUpdateDisplay.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("Move() returned" + eafRC.ToString(), "ZWO EAF Tool");
                            }
                        }
                        else
                        {
                            MessageBox.Show("GetPostion() returned" + eafRC.ToString(), "ZWO EAF Tool");

                        }

                        eafRC = EAFdll.Close(item.id);

                        if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            // everything worked
                        }
                        else
                        {
                            MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
                    }
                }
                else
                {
                    MessageBox.Show("No focuser selected", "ZWO EAF Tool");
                }
            }

        }

        private void mnuAddBookmark_Click(object sender, EventArgs e)
        {
            using (AddBookmark add = new AddBookmark())
            {
                EAFdll.EAF_ERROR_CODE eafRC;
                int currPos = 0;

                if (comboBoxFocuser.SelectedItem != null)
                {
                    EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                    eafRC = EAFdll.Open(item.id);

                    if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                    {

                        eafRC = EAFdll.GetPostion(item.id, out currPos);

                        if (eafRC != EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            MessageBox.Show("Couldn't get current position, rc = " + eafRC.ToString(), "ZWO EAF Tool");
                        }
                    }
                }
                else
                {
                    currPos = 0;
                }

                add.bookmark.position = currPos;

                DialogResult res = add.ShowDialog();

                if (res == DialogResult.OK)
                {

                    ToolStripMenuItem menuitem = new ToolStripMenuItem();

                    menuitem.Text = add.bookmark.name;
                    menuitem.Tag = add.bookmark;

                    cntxtmnuBookmarks.Items.Add(menuitem);
                }
            }
        }

        private void mnuEditBookmarks_Click(object sender, EventArgs e)
        {
            Form edit;

            edit = new EditBookmarks();

            edit.ShowDialog();
        }

        private void cntxtmnuBookmarks_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                Bookmark mark = (Bookmark)e.ClickedItem.Tag;

                MessageBox.Show("should be moving to position " + mark.position.ToString(), "Debug Message");
            }
            else
            {
                // MessageBox.Show("clicked on one of the menu items without a tag" , "Debug Message");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Save Menu Items here
            List<Bookmark> list = new List<Bookmark>();

            foreach(ToolStripMenuItem item in cntxtmnuBookmarks.Items)
            {
                if(item.Tag != null)
                {
                    list.Add((Bookmark)item.Tag);
                }
            }

            string json = JsonSerializer.Serialize(list);

            // MessageBox.Show("Serialized bookmark list = '" + json + "'", "Debug Message");

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string directory = folder + "\\" + settingsDirectory;

            if(!File.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string file = directory + "\\" + settingsFileName;

            File.WriteAllText(file, json);
        }
    }
}

internal class EAFComboBoxItem
{
    public int id;

    public EAFComboBoxItem(EAFdll.EAF_INFO eafinfo)
    {
        id = eafinfo.ID;
    }

    public override string ToString()
    {
        string name;

        name = "EAF (ID:" + id.ToString() + ")";

        return name;
    }
}



