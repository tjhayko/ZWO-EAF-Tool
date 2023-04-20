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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFocuser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.txtTargetFocuserPosition = new System.Windows.Forms.MaskedTextBox();
            this.tmrUpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblFocuserMoving = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFocuserPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Focuser";
            // 
            // comboBoxFocuser
            // 
            this.comboBoxFocuser.FormattingEnabled = true;
            this.comboBoxFocuser.Location = new System.Drawing.Point(178, 66);
            this.comboBoxFocuser.Name = "comboBoxFocuser";
            this.comboBoxFocuser.Size = new System.Drawing.Size(230, 28);
            this.comboBoxFocuser.TabIndex = 1;
            this.comboBoxFocuser.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFocuser_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target HW Position";
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Enabled = false;
            this.btnSetPosition.Location = new System.Drawing.Point(295, 134);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(113, 34);
            this.btnSetPosition.TabIndex = 3;
            this.btnSetPosition.Text = "Set Position";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // btnMoveTo
            // 
            this.btnMoveTo.Enabled = false;
            this.btnMoveTo.Location = new System.Drawing.Point(414, 134);
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.Size = new System.Drawing.Size(140, 34);
            this.btnMoveTo.TabIndex = 4;
            this.btnMoveTo.Text = "Move To Position";
            this.btnMoveTo.UseVisualStyleBackColor = true;
            this.btnMoveTo.Click += new System.EventHandler(this.btnMoveTo_Click);
            // 
            // txtTargetFocuserPosition
            // 
            this.txtTargetFocuserPosition.Location = new System.Drawing.Point(178, 140);
            this.txtTargetFocuserPosition.Mask = "000000";
            this.txtTargetFocuserPosition.Name = "txtTargetFocuserPosition";
            this.txtTargetFocuserPosition.Size = new System.Drawing.Size(100, 26);
            this.txtTargetFocuserPosition.TabIndex = 2;
            this.txtTargetFocuserPosition.ValidatingType = typeof(int);
            // 
            // tmrUpdateDisplay
            // 
            this.tmrUpdateDisplay.Interval = 500;
            this.tmrUpdateDisplay.Tick += new System.EventHandler(this.UpdateDisplay);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Is Focuser Moving?";
            // 
            // lblFocuserMoving
            // 
            this.lblFocuserMoving.AutoSize = true;
            this.lblFocuserMoving.Location = new System.Drawing.Point(174, 178);
            this.lblFocuserMoving.Name = "lblFocuserMoving";
            this.lblFocuserMoving.Size = new System.Drawing.Size(29, 20);
            this.lblFocuserMoving.TabIndex = 8;
            this.lblFocuserMoving.Text = "No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Focuser HW Position";
            // 
            // lblFocuserPosition
            // 
            this.lblFocuserPosition.AutoSize = true;
            this.lblFocuserPosition.Location = new System.Drawing.Point(174, 105);
            this.lblFocuserPosition.Name = "lblFocuserPosition";
            this.lblFocuserPosition.Size = new System.Drawing.Size(158, 20);
            this.lblFocuserPosition.TabIndex = 10;
            this.lblFocuserPosition.Text = "No Focuser Selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(298, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "ZWO EAF Tool © Tom Hayko";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 219);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFocuserPosition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFocuserMoving);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTargetFocuserPosition);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.btnSetPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFocuser);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "ZWO EAF Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFocuserMoving;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFocuserPosition;
        private System.Windows.Forms.Label label5;
    }
}

