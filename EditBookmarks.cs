using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using ZWO_EAF_Tool.Properties;
using System.Data.Common;

namespace ZWO_EAF_Tool
{
    public partial class EditBookmarks : Form
    {
        private ContextMenuStrip menuStrip;
        private DataTable tableBookmarks = new DataTable("Bookmarks");

        private void initDatatable()
        {
            DataColumn column;

            column = new DataColumn("Name");

            tableBookmarks.Columns.Add(column);

            column = new DataColumn("Position");

            tableBookmarks.Columns.Add(column);
        }

        public EditBookmarks()
        {
            InitializeComponent();

            initDatatable();
        }

        public EditBookmarks(ContextMenuStrip menu)
        {
            InitializeComponent();
            menuStrip = menu;

            initDatatable();
        }

        private void EditBookmarks_Load(object sender, EventArgs e)
        {
            DataRow rowBookmark;

            foreach (ToolStripMenuItem item in menuStrip.Items)
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

            column.Width = 125;

            column = datagridBookmarks.Columns[1];

            column.Width = 100;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
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

            for (int idx = menuStrip.Items.Count - 1 ; idx >= 0 ; idx--)
            {
                if (menuStrip.Items[idx].Tag != null)
                {
                    menuStrip.Items.Remove(menuStrip.Items[idx]);
                }
            }

            bool bError = false;

            foreach(DataRow row in tableBookmarks.Rows)
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

                    menuStrip.Items.Add(menuItem);
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
                this.Close();
            }
        }
    }
}
