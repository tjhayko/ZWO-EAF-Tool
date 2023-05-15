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
using System.Text.Json.Nodes;
using System.Xml;
using System.Diagnostics.Eventing.Reader;

// using code from here https://stackoverflow.com/questions/683330/how-to-make-a-window-always-stay-on-top-in-net

namespace ZWO_EAF_Tool
{
    public partial class MainForm : Form
    {
        const int normalFormHeight = 175; 
        const int normalFormWidth = 290;

        const int editFormHeight = 369;
        const int EditFormWidth = 290;

        private DataTable tableBookmarks = new DataTable("Bookmarks");

        const string settingsDirectory = "ZWOEAFTool";
        const string settingsFileName = "ZWOEAFTool.json";

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        

        private void initDatatable()
        {
            DataColumn column;

            column = new DataColumn("Name");

            tableBookmarks.Columns.Add(column);

            column = new DataColumn("Position");

            tableBookmarks.Columns.Add(column);
        }

        private void moveTo(int id, int pos)
        {
            EAF_ERROR_CODE eafRC;

            eafRC = EAFdll.Open(id);

            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.Move(id, pos);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    // Move worked
                    lblFocuserMoving.Text = "Moving";

                    tmrUpdateDisplay.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Move() failed, rc = " + eafRC.ToString(), "EAF Tool");
                }

                eafRC = EAFdll.Close(id);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {

                }
                else
                {
                    MessageBox.Show("Close() failed, rc = " + eafRC.ToString(), "EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("Open() failed, rc = " + eafRC.ToString());
            }
        }

        private void moveRelative(int id, int pos)
        {
            EAF_ERROR_CODE eafRC;
            int currPos;

            eafRC = EAFdll.Open(id);

            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.GetPostion(id, out currPos);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    pos = currPos + pos;

                    if (pos >= 0)
                    {
                        eafRC = EAFdll.Move(id, pos);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {
                            // Move worked
                            lblFocuserMoving.Text = "Moving";

                            tmrUpdateDisplay.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Move() failed, rc = " + eafRC.ToString(), "EAF Tool");
                        }

                        eafRC = EAFdll.Close(id);

                        if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Close() failed, rc = " + eafRC.ToString(), "EAF Tool");
                    }
                }
            }
            else
            {
                MessageBox.Show("Open() failed, rc = " + eafRC.ToString());
            }

        }

        private void setPosition(int id, int pos)
        {
            EAF_ERROR_CODE eafRC;

            eafRC = EAFdll.Open(id);

            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.ResetPostion(id, pos);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    MessageBox.Show("New Position set to " + pos.ToString(), "ZWO EAF Tool");
                    lblFocuserPosition.Text = pos.ToString();
                }
                else
                {
                    MessageBox.Show("ResetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }

                eafRC = EAFdll.Close(id);

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

        private int getPosition(int id)
        {
            EAF_ERROR_CODE eafRC;
            int pos = 0;

            btnMoveTo.Enabled = true;
            btnSetPosition.Enabled = true;

            eafRC = EAFdll.Open(id);

            if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.GetPostion(id, out pos);

                if (eafRC == EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                }
                else
                {
                    MessageBox.Show("GetPosition() returned " + eafRC.ToString(), "ZWO EAF Tool");
                }

                eafRC = EAFdll.Close(id);

                if (eafRC != EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("Open() returned " + eafRC.ToString(), "ZWO EAF Tool");
            }
            return pos;
        }

        private void stopFocuser(int id)
        {
            EAF_ERROR_CODE eafRC;

            eafRC = EAFdll.Open(id);

            if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
            {
                eafRC = EAFdll.Stop(id);

                if (eafRC == EAF_ERROR_CODE.EAF_SUCCESS)
                {

                }
                else
                {
                    MessageBox.Show("Error, Stop() returned" + eafRC.ToString(), "EAF Tool");
                }

                eafRC = EAFdll.Close(id);

                if (eafRC != EAFdll.EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    MessageBox.Show("Close() returned" + eafRC.ToString(), "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("Error, Open returned " + eafRC.ToString(), "EAF Tool");
            }
        }

        private void addMenusFromSettings()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string file = folder + "\\" + settingsDirectory + "\\" + settingsFileName;

            if (File.Exists(file))
            {
                string jsonSettings = File.ReadAllText(file);

                List<Bookmark> bookmarks = new List<Bookmark>();

                bookmarks = JsonSerializer.Deserialize<List<Bookmark>>(jsonSettings);

                foreach (Bookmark b in bookmarks)
                {
                    ToolStripMenuItem t = new ToolStripMenuItem();

                    t.Tag = b;
                    t.Text = b.name;

                    cntxtmnuBookmarks.Items.Add(t);
                }
            }
        }

        private void readSettings()
        {
            JsonDocument doc;

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string file = folder + "\\" + settingsDirectory + "\\" + settingsFileName;

            if (!File.Exists(file))
            {
                // Nothing to read
            }
            else
            {
                string str = File.ReadAllText(file);

                doc = JsonDocument.Parse(str);

                txtBoxStep.Text = doc.RootElement.GetProperty("StepSize").GetProperty("Value").GetString();

                int count = 0;

                try
                {
                    while (true)
                    {
                        Bookmark b = new Bookmark(); ;
                        string propName = "Menu" + count.ToString();

                        JsonElement elem = doc.RootElement.GetProperty(propName);

                        b.name = elem.GetProperty("Name").GetString();
                        b.position = elem.GetProperty("Position").GetInt32();

                        ToolStripMenuItem t = new ToolStripMenuItem();

                        t.Tag = b;
                        t.Text = b.name;

                        cntxtmnuBookmarks.Items.Add(t);

                        count++;
                    }
                }
                catch (KeyNotFoundException keyNotFound)
                {
                    // means we've reached the end of the menu items

                    // Remove compiler warnings here

                    string s = keyNotFound.Message;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            // FormBorderStyle = FormBorderStyle.FixedSingle;
            // TopMost = true;

            // addMenusFromSettings();

            readSettings();
            initDatatable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int iEAFCount;
            EAFdll.EAF_ERROR_CODE rc;
            EAFdll.EAF_INFO eafinfo;
            int id;

            int i;

            // resize form to hide edit bookmarks controls

            this.Width = normalFormWidth;
            this.Height = normalFormHeight;

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
            int pos;

            EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

            btnMoveTo.Enabled = true;
            btnSetPosition.Enabled = true;

            pos = getPosition(item.id);

            lblFocuserPosition.Text = pos.ToString();
            txtTargetFocuserPosition.Text = pos.ToString();
        }

        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            if (comboBoxFocuser.SelectedItem != null)
            {
                EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                int iNewPos;

                if (Int32.TryParse(txtTargetFocuserPosition.Text, out iNewPos))
                {
                    setPosition(item.id, iNewPos);
                }
                else
                {
                    MessageBox.Show("text '" + txtTargetFocuserPosition.Text + "' couldn't be parsed to an integer", "ZWO EAF Tool");
                }
            }
            else
            {
                MessageBox.Show("No focuser selected", "ZWO EAF Tool");
            }
        }

        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            int iNewPos = 0;

            if (comboBoxFocuser.SelectedItem != null)
            {
                EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                if (Int32.TryParse(txtTargetFocuserPosition.Text, out iNewPos))
                {
                    moveTo(item.id, iNewPos);
                }
                else
                {
                    MessageBox.Show("text '" + txtTargetFocuserPosition.Text + "' couldn't be parsed to an integer", "ZWO EAF Tool");
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
                    if (bSet || bHC)
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

                if (eafRC != EAF_ERROR_CODE.EAF_SUCCESS)
                {
                    // TODO: thown an exception here instead

                    MessageBox.Show("IsMoving() returned " + eafRC.ToString());
                    break;
                }
                else
                {
                    if (bIsMoving && bIsHCMoving)
                    {
                        Thread.Sleep(200);
                    }
                }
            } while (bIsMoving || bIsHCMoving);
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

                    moveRelative(item.id, -iStep);
                }
                else
                {
                    MessageBox.Show("No focuser selected", "ZWO EAF Tool");
                }
            }

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
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

                    moveRelative(item.id, iStep);
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


            // edit = new EditBookmarks(cntxtmnuBookmarks);

            // edit.ShowDialog();

            this.Height = 400;
            this.Width = 280;
        }

        private void cntxtmnuBookmarks_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                //TODO: add code to actually move the position

                Bookmark mark = (Bookmark)e.ClickedItem.Tag;

                if (comboBoxFocuser.SelectedItem != null)
                {
                    EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                    moveTo(item.id, mark.position);
                }
                else
                {
                    MessageBox.Show("No focuser selected, can't move to bookmark", "ZWO EAF Tool");
                }
            }
            else
            {
                // MessageBox.Show("clicked on one of the menu items without a tag" , "Debug Message");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Creating custom JSON object instead of serialized list work in progress

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string directory = folder + "\\" + settingsDirectory;

            if (!File.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string file = directory + "\\" + settingsFileName;

            JsonObject settings = new JsonObject();

            settings!["StepSize"] = new JsonObject { ["Value"] = txtBoxStep.Text };

            int count = 0;

            foreach (ToolStripMenuItem item in cntxtmnuBookmarks.Items)
            {
                if (item.Tag != null)
                {
                    Bookmark b = (Bookmark)item.Tag;

                    settings!["Menu" + count.ToString()] = new JsonObject { ["Name"] = b.name, ["Position"] = b.position };

                    count++;
                }
            }

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            File.WriteAllText(file, settings.ToJsonString(options));

            JsonNode tmp = settings!["Menu" + (count - 1).ToString()];
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (comboBoxFocuser.SelectedItem != null)
            {
                EAFComboBoxItem item = (EAFComboBoxItem)comboBoxFocuser.SelectedItem;

                stopFocuser(item.id);
            }
        }

        private void btnEditBookmarks_Click(object sender, EventArgs e)
        {
            if (btnEditBookmarks.Text == "Edit Bookmarks")
            {
                this.Height = editFormHeight;
                this.Width = EditFormWidth;

                btnEditBookmarks.Text = "Stop Editing";

                DataRow rowBookmark;

                foreach (ToolStripMenuItem item in cntxtmnuBookmarks.Items)
                {
                    if (item.Tag != null)
                    {
                        rowBookmark = tableBookmarks.NewRow();

                        Bookmark b = (Bookmark)item.Tag;

                        rowBookmark["Name"] = b.name;
                        rowBookmark["Position"] = b.position.ToString();

                        // MessageBox.Show("Row = '" + b.name + "' | '" + b.position.ToString() + "'");

                        tableBookmarks.Rows.Add(rowBookmark);
                    }
                }

                datagridBookmarks.DataSource = tableBookmarks;

                DataGridViewColumn column;

                column = datagridBookmarks.Columns[0];

                column.Width = 100;

                column = datagridBookmarks.Columns[1];

                column.Width = 75;

                datagridBookmarks.Enabled = true;
            }
            else
            {
                Bookmark b;

                // Remove the menu items that where added

                /* foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if (item.Tag != null)
                    {
                        menuStrip.Items.Remove(item);
                    }
                } */

                for (int idx = cntxtmnuBookmarks.Items.Count - 1; idx >= 0; idx--)
                {
                    if (cntxtmnuBookmarks.Items[idx].Tag != null)
                    {
                        cntxtmnuBookmarks.Items.Remove(cntxtmnuBookmarks.Items[idx]);
                    }
                }

                bool bError = false;

                foreach (DataRow row in tableBookmarks.Rows)
                {
                    int pos;

                    ToolStripMenuItem menuItem = new ToolStripMenuItem();

                    if (Int32.TryParse(row["Position"].ToString(), out pos))
                    {
                        b = new Bookmark();

                        b.name = row["Name"].ToString();

                        b.position = pos;

                        menuItem.Tag = b;

                        menuItem.Text = b.name;

                        cntxtmnuBookmarks.Items.Add(menuItem);
                    }
                    else
                    {
                        MessageBox.Show("position is not an valid number", "ZWO EAF Tool");

                        bError = true;

                        // TODO: select the row

                        break;
                    }
                }

                if (!bError)
                {
                    btnEditBookmarks.Text = "Edit Bookmarks";

                    this.Width = normalFormWidth;
                    this.Height = normalFormHeight;

                    tableBookmarks.Rows.Clear();

                    datagridBookmarks.Enabled = false;
                }
            }
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



