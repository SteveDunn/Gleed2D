namespace Gleed2D.Core.Forms
{
    partial class AddCustomPropertyForm
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
			this.uiNameTextBox = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.uiDescriptionTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.uiStringRadioButton = new System.Windows.Forms.RadioButton();
			this.uiBooleanRadioButton = new System.Windows.Forms.RadioButton();
			this.uiVector2RadioButton = new System.Windows.Forms.RadioButton();
			this.uiColorRadioButton = new System.Windows.Forms.RadioButton();
			this.uiLinkedItemRadioButton = new System.Windows.Forms.RadioButton();
			this.uiErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.uiErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// uiNameTextBox
			// 
			this.uiNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiNameTextBox.Location = new System.Drawing.Point(96, 10);
			this.uiNameTextBox.Name = "uiNameTextBox";
			this.uiNameTextBox.Size = new System.Drawing.Size(221, 20);
			this.uiNameTextBox.TabIndex = 1;
			this.uiNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(243, 235);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancelClick);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Enabled = false;
			this.buttonOK.Location = new System.Drawing.Point(162, 235);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOkClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Description";
			// 
			// uiDescriptionTextBox
			// 
			this.uiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiDescriptionTextBox.Location = new System.Drawing.Point(96, 36);
			this.uiDescriptionTextBox.Multiline = true;
			this.uiDescriptionTextBox.Name = "uiDescriptionTextBox";
			this.uiDescriptionTextBox.Size = new System.Drawing.Size(221, 78);
			this.uiDescriptionTextBox.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Type";
			// 
			// uiStringRadioButton
			// 
			this.uiStringRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uiStringRadioButton.AutoSize = true;
			this.uiStringRadioButton.Checked = true;
			this.uiStringRadioButton.Location = new System.Drawing.Point(96, 120);
			this.uiStringRadioButton.Name = "uiStringRadioButton";
			this.uiStringRadioButton.Size = new System.Drawing.Size(66, 17);
			this.uiStringRadioButton.TabIndex = 5;
			this.uiStringRadioButton.TabStop = true;
			this.uiStringRadioButton.Text = "Free text";
			this.uiStringRadioButton.UseVisualStyleBackColor = true;
			this.uiStringRadioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// uiBooleanRadioButton
			// 
			this.uiBooleanRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uiBooleanRadioButton.AutoSize = true;
			this.uiBooleanRadioButton.Location = new System.Drawing.Point(96, 143);
			this.uiBooleanRadioButton.Name = "uiBooleanRadioButton";
			this.uiBooleanRadioButton.Size = new System.Drawing.Size(64, 17);
			this.uiBooleanRadioButton.TabIndex = 6;
			this.uiBooleanRadioButton.Text = "Boolean";
			this.uiBooleanRadioButton.UseVisualStyleBackColor = true;
			// 
			// uiVector2RadioButton
			// 
			this.uiVector2RadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uiVector2RadioButton.AutoSize = true;
			this.uiVector2RadioButton.Location = new System.Drawing.Point(96, 166);
			this.uiVector2RadioButton.Name = "uiVector2RadioButton";
			this.uiVector2RadioButton.Size = new System.Drawing.Size(62, 17);
			this.uiVector2RadioButton.TabIndex = 7;
			this.uiVector2RadioButton.Text = "Vector2";
			this.uiVector2RadioButton.UseVisualStyleBackColor = true;
			// 
			// uiColorRadioButton
			// 
			this.uiColorRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uiColorRadioButton.AutoSize = true;
			this.uiColorRadioButton.Location = new System.Drawing.Point(96, 189);
			this.uiColorRadioButton.Name = "uiColorRadioButton";
			this.uiColorRadioButton.Size = new System.Drawing.Size(49, 17);
			this.uiColorRadioButton.TabIndex = 8;
			this.uiColorRadioButton.Text = "Color";
			this.uiColorRadioButton.UseVisualStyleBackColor = true;
			// 
			// uiLinkedItemRadioButton
			// 
			this.uiLinkedItemRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uiLinkedItemRadioButton.AutoSize = true;
			this.uiLinkedItemRadioButton.Location = new System.Drawing.Point(96, 212);
			this.uiLinkedItemRadioButton.Name = "uiLinkedItemRadioButton";
			this.uiLinkedItemRadioButton.Size = new System.Drawing.Size(79, 17);
			this.uiLinkedItemRadioButton.TabIndex = 9;
			this.uiLinkedItemRadioButton.Text = "&Linked item";
			this.uiLinkedItemRadioButton.UseVisualStyleBackColor = true;
			// 
			// uiErrorProvider
			// 
			this.uiErrorProvider.ContainerControl = this;
			// 
			// AddCustomPropertyForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(330, 270);
			this.ControlBox = false;
			this.Controls.Add(this.uiLinkedItemRadioButton);
			this.Controls.Add(this.uiColorRadioButton);
			this.Controls.Add(this.uiVector2RadioButton);
			this.Controls.Add(this.uiBooleanRadioButton);
			this.Controls.Add(this.uiStringRadioButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.uiDescriptionTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.uiNameTextBox);
			this.Controls.Add(this.label1);
			this.Name = "AddCustomPropertyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Custom Property";
			this.Load += new System.EventHandler(this.AddCustomProperty_Load);
			((System.ComponentModel.ISupportInitialize)(this.uiErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiNameTextBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiDescriptionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton uiStringRadioButton;
        private System.Windows.Forms.RadioButton uiBooleanRadioButton;
        private System.Windows.Forms.RadioButton uiVector2RadioButton;
        private System.Windows.Forms.RadioButton uiColorRadioButton;
        private System.Windows.Forms.RadioButton uiLinkedItemRadioButton;
		private System.Windows.Forms.ErrorProvider uiErrorProvider;
    }
}