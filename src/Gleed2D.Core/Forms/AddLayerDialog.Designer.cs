namespace Gleed2D.Core.Forms
{
    partial class AddLayerDialog
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
			this.uiButtonOk = new System.Windows.Forms.Button();
			this.uiErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.uiErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Layer &name";
			// 
			// uiNameTextBox
			// 
			this.uiNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiNameTextBox.Location = new System.Drawing.Point(83, 10);
			this.uiNameTextBox.Name = "uiNameTextBox";
			this.uiNameTextBox.Size = new System.Drawing.Size(227, 20);
			this.uiNameTextBox.TabIndex = 0;
			this.uiNameTextBox.TextChanged += new System.EventHandler(this.uiNameTextBoxTextChanged);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(235, 37);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// uiButtonOk
			// 
			this.uiButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uiButtonOk.Enabled = false;
			this.uiButtonOk.Location = new System.Drawing.Point(154, 37);
			this.uiButtonOk.Name = "uiButtonOk";
			this.uiButtonOk.Size = new System.Drawing.Size(75, 23);
			this.uiButtonOk.TabIndex = 1;
			this.uiButtonOk.Text = "OK";
			this.uiButtonOk.UseVisualStyleBackColor = true;
			this.uiButtonOk.Click += new System.EventHandler(this.uiButtonOk_Click);
			// 
			// uiErrorProvider
			// 
			this.uiErrorProvider.ContainerControl = this;
			// 
			// AddLayerDialog
			// 
			this.AcceptButton = this.uiButtonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(322, 68);
			this.ControlBox = false;
			this.Controls.Add(this.uiButtonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.uiNameTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "AddLayerDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Layer";
			((System.ComponentModel.ISupportInitialize)(this.uiErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiNameTextBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button uiButtonOk;
		private System.Windows.Forms.ErrorProvider uiErrorProvider;
    }
}