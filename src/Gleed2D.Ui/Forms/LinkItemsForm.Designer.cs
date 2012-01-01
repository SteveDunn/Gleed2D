namespace GLEED2D.Forms
{
    partial class LinkItemsForm
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
			this.uiButtonCancel = new System.Windows.Forms.Button();
			this.uiButtonOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.uiFirstItemCustomPropertyNameTextBox = new System.Windows.Forms.TextBox();
			this.uiLinkSecondItemToFirstCheckbox = new System.Windows.Forms.CheckBox();
			this.uiFirstItemGroupBox = new System.Windows.Forms.GroupBox();
			this.uiSecondItemGroupBox = new System.Windows.Forms.GroupBox();
			this.uiSameAsFirstItemCheckbox = new System.Windows.Forms.CheckBox();
			this.uiSecondItemCustomPropertyNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.uiFirstItemGroupBox.SuspendLayout();
			this.uiSecondItemGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiButtonCancel
			// 
			this.uiButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uiButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.uiButtonCancel.Location = new System.Drawing.Point(294, 212);
			this.uiButtonCancel.Name = "uiButtonCancel";
			this.uiButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.uiButtonCancel.TabIndex = 0;
			this.uiButtonCancel.Text = "Cancel";
			this.uiButtonCancel.UseVisualStyleBackColor = true;
			// 
			// uiButtonOk
			// 
			this.uiButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uiButtonOk.Location = new System.Drawing.Point(213, 212);
			this.uiButtonOk.Name = "uiButtonOk";
			this.uiButtonOk.Size = new System.Drawing.Size(75, 23);
			this.uiButtonOk.TabIndex = 1;
			this.uiButtonOk.Text = "OK";
			this.uiButtonOk.UseVisualStyleBackColor = true;
			this.uiButtonOk.Click += new System.EventHandler(this.buttonOkClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Custom Property Name:";
			// 
			// uiFirstItemCustomPropertyNameTextBox
			// 
			this.uiFirstItemCustomPropertyNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiFirstItemCustomPropertyNameTextBox.Location = new System.Drawing.Point(130, 24);
			this.uiFirstItemCustomPropertyNameTextBox.Name = "uiFirstItemCustomPropertyNameTextBox";
			this.uiFirstItemCustomPropertyNameTextBox.Size = new System.Drawing.Size(213, 20);
			this.uiFirstItemCustomPropertyNameTextBox.TabIndex = 3;
			this.uiFirstItemCustomPropertyNameTextBox.TextChanged += new System.EventHandler(this.uiFirstItemCustomPropertyNameTextBoxTextChanged);
			// 
			// uiLinkSecondItemToFirstCheckbox
			// 
			this.uiLinkSecondItemToFirstCheckbox.AutoSize = true;
			this.uiLinkSecondItemToFirstCheckbox.Location = new System.Drawing.Point(12, 94);
			this.uiLinkSecondItemToFirstCheckbox.Name = "uiLinkSecondItemToFirstCheckbox";
			this.uiLinkSecondItemToFirstCheckbox.Size = new System.Drawing.Size(188, 17);
			this.uiLinkSecondItemToFirstCheckbox.TabIndex = 4;
			this.uiLinkSecondItemToFirstCheckbox.Text = "Link second Item back to first Item";
			this.uiLinkSecondItemToFirstCheckbox.UseVisualStyleBackColor = true;
			this.uiLinkSecondItemToFirstCheckbox.CheckedChanged += new System.EventHandler(this.uiLinkSecondItemToFirstCheckboxCheckChanged);
			// 
			// uiFirstItemGroupBox
			// 
			this.uiFirstItemGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiFirstItemGroupBox.Controls.Add(this.uiFirstItemCustomPropertyNameTextBox);
			this.uiFirstItemGroupBox.Controls.Add(this.label1);
			this.uiFirstItemGroupBox.Location = new System.Drawing.Point(12, 12);
			this.uiFirstItemGroupBox.Name = "uiFirstItemGroupBox";
			this.uiFirstItemGroupBox.Size = new System.Drawing.Size(350, 76);
			this.uiFirstItemGroupBox.TabIndex = 7;
			this.uiFirstItemGroupBox.TabStop = false;
			this.uiFirstItemGroupBox.Text = "First Item ($f)";
			// 
			// uiSecondItemGroupBox
			// 
			this.uiSecondItemGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiSecondItemGroupBox.Controls.Add(this.uiSameAsFirstItemCheckbox);
			this.uiSecondItemGroupBox.Controls.Add(this.uiSecondItemCustomPropertyNameTextBox);
			this.uiSecondItemGroupBox.Controls.Add(this.label2);
			this.uiSecondItemGroupBox.Location = new System.Drawing.Point(12, 117);
			this.uiSecondItemGroupBox.Name = "uiSecondItemGroupBox";
			this.uiSecondItemGroupBox.Size = new System.Drawing.Size(350, 80);
			this.uiSecondItemGroupBox.TabIndex = 7;
			this.uiSecondItemGroupBox.TabStop = false;
			this.uiSecondItemGroupBox.Text = "Second Item ($s)";
			// 
			// uiSameAsFirstItemCheckbox
			// 
			this.uiSameAsFirstItemCheckbox.AutoSize = true;
			this.uiSameAsFirstItemCheckbox.Checked = true;
			this.uiSameAsFirstItemCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uiSameAsFirstItemCheckbox.Enabled = false;
			this.uiSameAsFirstItemCheckbox.Location = new System.Drawing.Point(130, 52);
			this.uiSameAsFirstItemCheckbox.Name = "uiSameAsFirstItemCheckbox";
			this.uiSameAsFirstItemCheckbox.Size = new System.Drawing.Size(107, 17);
			this.uiSameAsFirstItemCheckbox.TabIndex = 4;
			this.uiSameAsFirstItemCheckbox.Text = "same as first Item";
			this.uiSameAsFirstItemCheckbox.UseVisualStyleBackColor = true;
			this.uiSameAsFirstItemCheckbox.CheckedChanged += new System.EventHandler(this.uiSameAsFirstItemCheckboxCheckChanged);
			// 
			// uiSecondItemCustomPropertyNameTextBox
			// 
			this.uiSecondItemCustomPropertyNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiSecondItemCustomPropertyNameTextBox.Enabled = false;
			this.uiSecondItemCustomPropertyNameTextBox.Location = new System.Drawing.Point(130, 25);
			this.uiSecondItemCustomPropertyNameTextBox.Name = "uiSecondItemCustomPropertyNameTextBox";
			this.uiSecondItemCustomPropertyNameTextBox.Size = new System.Drawing.Size(213, 20);
			this.uiSecondItemCustomPropertyNameTextBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(6, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Custom Property Name:";
			// 
			// LinkItemsForm
			// 
			this.AcceptButton = this.uiButtonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.uiButtonCancel;
			this.ClientSize = new System.Drawing.Size(381, 247);
			this.ControlBox = false;
			this.Controls.Add(this.uiSecondItemGroupBox);
			this.Controls.Add(this.uiFirstItemGroupBox);
			this.Controls.Add(this.uiLinkSecondItemToFirstCheckbox);
			this.Controls.Add(this.uiButtonOk);
			this.Controls.Add(this.uiButtonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LinkItemsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Link Items by a Custom Property";
			this.VisibleChanged += new System.EventHandler(this.linkItemsFormVisibleChanged);
			this.uiFirstItemGroupBox.ResumeLayout(false);
			this.uiFirstItemGroupBox.PerformLayout();
			this.uiSecondItemGroupBox.ResumeLayout(false);
			this.uiSecondItemGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uiButtonCancel;
        private System.Windows.Forms.Button uiButtonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiFirstItemCustomPropertyNameTextBox;
        private System.Windows.Forms.CheckBox uiLinkSecondItemToFirstCheckbox;
        private System.Windows.Forms.GroupBox uiFirstItemGroupBox;
        private System.Windows.Forms.GroupBox uiSecondItemGroupBox;
        private System.Windows.Forms.TextBox uiSecondItemCustomPropertyNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox uiSameAsFirstItemCheckbox;
    }
}