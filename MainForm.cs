using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZWOptical.EAFSDK;
using static ZWOptical.EAFSDK.EAFdll;


namespace ZWO_EAF_Tool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
                        lblFocuserMoving.Text = "Yes";
                    }
                    else
                    {
                        lblFocuserMoving.Text = "No";

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

