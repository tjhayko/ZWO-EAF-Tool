namespace ZWO_EAF_Tool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFocuser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.txtTargetFocuserPosition = new System.Windows.Forms.MaskedTextBox();
            this.tmrUpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.lblFocuserMoving = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFocuserPosition = new System.Windows.Forms.Label();
            this.chkStayOnTop = new System.Windows.Forms.CheckBox();
            this.cntxtmnuBookmarks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxStep = new System.Windows.Forms.MaskedTextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.datagridBookmarks = new System.Windows.Forms.DataGridView();
            this.btnEditBookmarks = new System.Windows.Forms.Button();
            this.btnUpdateMenu = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.splitButton1 = new ZWO_EAF_Tool.SplitButton();
            this.cntxtmnuBookmarks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridBookmarks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Focuser";
            // 
            // comboBoxFocuser
            // 
            this.comboBoxFocuser.FormattingEnabled = true;
            this.comboBoxFocuser.Location = new System.Drawing.Point(95, 14);
            this.comboBoxFocuser.Name = "comboBoxFocuser";
            this.comboBoxFocuser.Size = new System.Drawing.Size(109, 28);
            this.comboBoxFocuser.TabIndex = 1;
            this.comboBoxFocuser.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFocuser_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target";
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Enabled = false;
            this.btnSetPosition.Location = new System.Drawing.Point(171, 86);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(55, 34);
            this.btnSetPosition.TabIndex = 6;
            this.btnSetPosition.Text = "Set";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // btnMoveTo
            // 
            this.btnMoveTo.Enabled = false;
            this.btnMoveTo.Location = new System.Drawing.Point(239, 86);
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.Size = new System.Drawing.Size(60, 34);
            this.btnMoveTo.TabIndex = 7;
            this.btnMoveTo.Text = "Goto";
            this.btnMoveTo.UseVisualStyleBackColor = true;
            this.btnMoveTo.Click += new System.EventHandler(this.btnMoveTo_Click);
            // 
            // txtTargetFocuserPosition
            // 
            this.txtTargetFocuserPosition.Location = new System.Drawing.Point(97, 88);
            this.txtTargetFocuserPosition.Mask = "000000";
            this.txtTargetFocuserPosition.Name = "txtTargetFocuserPosition";
            this.txtTargetFocuserPosition.Size = new System.Drawing.Size(67, 26);
            this.txtTargetFocuserPosition.TabIndex = 5;
            this.txtTargetFocuserPosition.ValidatingType = typeof(int);
            // 
            // tmrUpdateDisplay
            // 
            this.tmrUpdateDisplay.Tick += new System.EventHandler(this.UpdateDisplay);
            // 
            // lblFocuserMoving
            // 
            this.lblFocuserMoving.AutoSize = true;
            this.lblFocuserMoving.Location = new System.Drawing.Point(210, 18);
            this.lblFocuserMoving.Name = "lblFocuserMoving";
            this.lblFocuserMoving.Size = new System.Drawing.Size(116, 20);
            this.lblFocuserMoving.TabIndex = 8;
            this.lblFocuserMoving.Text = "Not Connected";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Position";
            // 
            // lblFocuserPosition
            // 
            this.lblFocuserPosition.AutoSize = true;
            this.lblFocuserPosition.Location = new System.Drawing.Point(122, 54);
            this.lblFocuserPosition.Name = "lblFocuserPosition";
            this.lblFocuserPosition.Size = new System.Drawing.Size(63, 20);
            this.lblFocuserPosition.TabIndex = 10;
            this.lblFocuserPosition.Text = "000000";
            // 
            // chkStayOnTop
            // 
            this.chkStayOnTop.AutoSize = true;
            this.chkStayOnTop.Location = new System.Drawing.Point(95, 168);
            this.chkStayOnTop.Name = "chkStayOnTop";
            this.chkStayOnTop.Size = new System.Drawing.Size(120, 24);
            this.chkStayOnTop.TabIndex = 11;
            this.chkStayOnTop.Text = "Stay on Top";
            this.chkStayOnTop.UseVisualStyleBackColor = true;
            this.chkStayOnTop.CheckedChanged += new System.EventHandler(this.chkStayOnTop_CheckedChanged);
            // 
            // cntxtmnuBookmarks
            // 
            this.cntxtmnuBookmarks.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cntxtmnuBookmarks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddBookmark,
            this.saveToolStripMenuItem});
            this.cntxtmnuBookmarks.Name = "contextMenuStrip1";
            this.cntxtmnuBookmarks.Size = new System.Drawing.Size(216, 68);
            this.cntxtmnuBookmarks.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cntxtmnuBookmarks_ItemClicked);
            // 
            // mnuAddBookmark
            // 
            this.mnuAddBookmark.Name = "mnuAddBookmark";
            this.mnuAddBookmark.Size = new System.Drawing.Size(215, 32);
            this.mnuAddBookmark.Text = "Add";
            this.mnuAddBookmark.Click += new System.EventHandler(this.mnuAddBookmark_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(215, 32);
            this.saveToolStripMenuItem.Text = "Save Bookmarks";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(95, 47);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(25, 34);
            this.btnMoveDown.TabIndex = 2;
            this.btnMoveDown.Text = "<";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(191, 47);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(25, 34);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = ">";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(223, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Step";
            // 
            // txtBoxStep
            // 
            this.txtBoxStep.Location = new System.Drawing.Point(272, 53);
            this.txtBoxStep.Mask = "000000";
            this.txtBoxStep.Name = "txtBoxStep";
            this.txtBoxStep.Size = new System.Drawing.Size(67, 26);
            this.txtBoxStep.TabIndex = 4;
            this.txtBoxStep.Text = "1";
            this.txtBoxStep.ValidatingType = typeof(int);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(312, 87);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 34);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // datagridBookmarks
            // 
            this.datagridBookmarks.AllowUserToOrderColumns = true;
            this.datagridBookmarks.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.datagridBookmarks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridBookmarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridBookmarks.Enabled = false;
            this.datagridBookmarks.Location = new System.Drawing.Point(16, 214);
            this.datagridBookmarks.Name = "datagridBookmarks";
            this.datagridBookmarks.RowHeadersWidth = 62;
            this.datagridBookmarks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.datagridBookmarks.RowTemplate.Height = 28;
            this.datagridBookmarks.Size = new System.Drawing.Size(372, 275);
            this.datagridBookmarks.TabIndex = 12;
            // 
            // btnEditBookmarks
            // 
            this.btnEditBookmarks.Location = new System.Drawing.Point(238, 122);
            this.btnEditBookmarks.Name = "btnEditBookmarks";
            this.btnEditBookmarks.Size = new System.Drawing.Size(150, 34);
            this.btnEditBookmarks.TabIndex = 10;
            this.btnEditBookmarks.Text = "Edit Bookmarks";
            this.btnEditBookmarks.UseVisualStyleBackColor = true;
            this.btnEditBookmarks.Click += new System.EventHandler(this.btnEditBookmarks_Click);
            // 
            // btnUpdateMenu
            // 
            this.btnUpdateMenu.Enabled = false;
            this.btnUpdateMenu.Location = new System.Drawing.Point(17, 507);
            this.btnUpdateMenu.Name = "btnUpdateMenu";
            this.btnUpdateMenu.Size = new System.Drawing.Size(120, 65);
            this.btnUpdateMenu.TabIndex = 13;
            this.btnUpdateMenu.Text = "Update Menu";
            this.btnUpdateMenu.UseVisualStyleBackColor = true;
            this.btnUpdateMenu.Click += new System.EventHandler(this.btnUpdateMenu_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(144, 507);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 65);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Update Menu  && Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Enabled = false;
            this.btnDiscard.Location = new System.Drawing.Point(272, 507);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(120, 65);
            this.btnDiscard.TabIndex = 15;
            this.btnDiscard.Text = "Discard Changes";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // splitButton1
            // 
            this.splitButton1.Location = new System.Drawing.Point(94, 122);
            this.splitButton1.Menu = this.cntxtmnuBookmarks;
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.Size = new System.Drawing.Size(138, 34);
            this.splitButton1.TabIndex = 9;
            this.splitButton1.Text = "Bookmarks";
            this.splitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.splitButton1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 584);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUpdateMenu);
            this.Controls.Add(this.btnEditBookmarks);
            this.Controls.Add(this.datagridBookmarks);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtBoxStep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.splitButton1);
            this.Controls.Add(this.chkStayOnTop);
            this.Controls.Add(this.lblFocuserPosition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFocuserMoving);
            this.Controls.Add(this.txtTargetFocuserPosition);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.btnSetPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFocuser);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "ZWO EAF Tool © Tom Hayko";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.cntxtmnuBookmarks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridBookmarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFocuser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetPosition;
        private System.Windows.Forms.Button btnMoveTo;
        private System.Windows.Forms.MaskedTextBox txtTargetFocuserPosition;
        private System.Windows.Forms.Timer tmrUpdateDisplay;
        private System.Windows.Forms.Label lblFocuserMoving;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFocuserPosition;
        private System.Windows.Forms.CheckBox chkStayOnTop;
        private SplitButton splitButton1;
        private System.Windows.Forms.ContextMenuStrip cntxtmnuBookmarks;
        private System.Windows.Forms.ToolStripMenuItem mnuAddBookmark;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtBoxStep;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView datagridBookmarks;
        private System.Windows.Forms.Button btnEditBookmarks;
        private System.Windows.Forms.Button btnUpdateMenu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDiscard;
    }
}

