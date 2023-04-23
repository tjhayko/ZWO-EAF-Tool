using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZWO_EAF_Tool
{
    public partial class AddBookmark : Form
    {
        public Bookmark bookmark = new Bookmark();

        public AddBookmark()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Length > 0)
            {
                int pos;

                if(Int32.TryParse(txtValue.Text, out pos))
                {
                    bookmark.name = txtName.Text;
                    bookmark.position = pos;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ZWO EAF Tool", "Please enter a valid position");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddBookmark_Load(object sender, EventArgs e)
        {
            txtValue.Text = bookmark.position.ToString();
        }
    }
}
