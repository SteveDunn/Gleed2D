namespace GLEED2D.Forms
{
    partial class AboutForm
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
			this.uiCloseButton = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.uiFeedbackLinkLabel = new System.Windows.Forms.LinkLabel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.uiTabs = new System.Windows.Forms.TabControl();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.uiTabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiCloseButton
			// 
			this.uiCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uiCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.uiCloseButton.Location = new System.Drawing.Point(310, 375);
			this.uiCloseButton.Name = "uiCloseButton";
			this.uiCloseButton.Size = new System.Drawing.Size(107, 23);
			this.uiCloseButton.TabIndex = 1;
			this.uiCloseButton.Text = "Close";
			this.uiCloseButton.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(397, 335);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Application";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.uiFeedbackLinkLabel);
			this.panel1.Controls.Add(this.linkLabel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(397, 335);
			this.panel1.TabIndex = 3;
			// 
			// linkLabel2
			// 
			this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel2.Location = new System.Drawing.Point(3, 150);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(391, 23);
			this.linkLabel2.TabIndex = 2;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "https://github.com/SteveDunn/Gleed2D";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// uiFeedbackLinkLabel
			// 
			this.uiFeedbackLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiFeedbackLinkLabel.Location = new System.Drawing.Point(3, 206);
			this.uiFeedbackLinkLabel.Name = "uiFeedbackLinkLabel";
			this.uiFeedbackLinkLabel.Size = new System.Drawing.Size(391, 23);
			this.uiFeedbackLinkLabel.TabIndex = 2;
			this.uiFeedbackLinkLabel.TabStop = true;
			this.uiFeedbackLinkLabel.Text = "Feedback...";
			this.uiFeedbackLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.uiFeedbackLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(3, 3);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(391, 144);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "Gleed 2D \r\n\r\nGeneric Level Editor 2D\r\n\r\nVersion {v}\r\n\r\nby Steve Dunn \r\nfrom the o" +
    "riginal by \r\nzissakos (Zissis Siantidis)\r\n";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// uiTabs
			// 
			this.uiTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiTabs.Controls.Add(this.tabPage1);
			this.uiTabs.Location = new System.Drawing.Point(12, 12);
			this.uiTabs.Name = "uiTabs";
			this.uiTabs.SelectedIndex = 0;
			this.uiTabs.Size = new System.Drawing.Size(405, 361);
			this.uiTabs.TabIndex = 0;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.uiCloseButton;
			this.ClientSize = new System.Drawing.Size(429, 403);
			this.ControlBox = false;
			this.Controls.Add(this.uiCloseButton);
			this.Controls.Add(this.uiTabs);
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.tabPage1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.uiTabs.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button uiCloseButton;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.LinkLabel uiFeedbackLinkLabel;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.TabControl uiTabs;
    }
}