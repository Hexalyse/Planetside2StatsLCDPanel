namespace Planetside2LCDStats
{
    partial class Main
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
            this.btnShutdown = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.chkColor = new System.Windows.Forms.CheckBox();
            this.chkMono = new System.Windows.Forms.CheckBox();
            this.textBoxCharId = new System.Windows.Forms.TextBox();
            this.labelCharId = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxDisablePreview = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShutdown
            // 
            this.btnShutdown.Location = new System.Drawing.Point(121, 29);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(103, 23);
            this.btnShutdown.TabIndex = 5;
            this.btnShutdown.Text = "Shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 29);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(103, 23);
            this.btnInit.TabIndex = 4;
            this.btnInit.Text = "Start tracking";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(58, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status:";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // chkColor
            // 
            this.chkColor.AutoCheck = false;
            this.chkColor.AutoSize = true;
            this.chkColor.Location = new System.Drawing.Point(12, 81);
            this.chkColor.Name = "chkColor";
            this.chkColor.Size = new System.Drawing.Size(79, 17);
            this.chkColor.TabIndex = 9;
            this.chkColor.Text = "Color (G19)";
            this.chkColor.UseVisualStyleBackColor = true;
            // 
            // chkMono
            // 
            this.chkMono.AutoCheck = false;
            this.chkMono.AutoSize = true;
            this.chkMono.Location = new System.Drawing.Point(12, 58);
            this.chkMono.Name = "chkMono";
            this.chkMono.Size = new System.Drawing.Size(88, 17);
            this.chkMono.TabIndex = 8;
            this.chkMono.Text = "Monochrome";
            this.chkMono.UseVisualStyleBackColor = true;
            this.chkMono.CheckedChanged += new System.EventHandler(this.chkMono_CheckedChanged);
            // 
            // textBoxCharId
            // 
            this.textBoxCharId.Location = new System.Drawing.Point(306, 5);
            this.textBoxCharId.Name = "textBoxCharId";
            this.textBoxCharId.Size = new System.Drawing.Size(163, 20);
            this.textBoxCharId.TabIndex = 10;
            // 
            // labelCharId
            // 
            this.labelCharId.AutoSize = true;
            this.labelCharId.Location = new System.Drawing.Point(234, 8);
            this.labelCharId.Name = "labelCharId";
            this.labelCharId.Size = new System.Drawing.Size(68, 13);
            this.labelCharId.TabIndex = 11;
            this.labelCharId.Text = "Character Id:";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(366, 75);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(103, 23);
            this.buttonHelp.TabIndex = 12;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(200, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 43);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Preview:";
            // 
            // checkBoxDisablePreview
            // 
            this.checkBoxDisablePreview.AutoSize = true;
            this.checkBoxDisablePreview.Location = new System.Drawing.Point(135, 78);
            this.checkBoxDisablePreview.Name = "checkBoxDisablePreview";
            this.checkBoxDisablePreview.Size = new System.Drawing.Size(59, 17);
            this.checkBoxDisablePreview.TabIndex = 15;
            this.checkBoxDisablePreview.Text = "disable";
            this.checkBoxDisablePreview.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 115);
            this.Controls.Add(this.checkBoxDisablePreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.labelCharId);
            this.Controls.Add(this.textBoxCharId);
            this.Controls.Add(this.chkColor);
            this.Controls.Add(this.chkMono);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.btnInit);
            this.Name = "Main";
            this.Text = "Planetside 2 LCD Stats";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.CheckBox chkColor;
        private System.Windows.Forms.CheckBox chkMono;
        private System.Windows.Forms.TextBox textBoxCharId;
        private System.Windows.Forms.Label labelCharId;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxDisablePreview;
    }
}

