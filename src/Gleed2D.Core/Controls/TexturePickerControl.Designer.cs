namespace Gleed2D.Core.Controls
{
	partial class TexturePickerControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TexturePickerControl));
			this.label3 = new System.Windows.Forms.Label();
			this.uiItemsListView = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.uiThumbnailSizesCombo = new System.Windows.Forms.ComboBox();
			this.uiFolderText = new System.Windows.Forms.TextBox();
			this.fileSystemWatcher = new System.IO.FileSystemWatcher();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(560, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "thumbnails:";
			// 
			// uiItemsListView
			// 
			this.uiItemsListView.AllowDrop = true;
			this.uiItemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiItemsListView.HideSelection = false;
			this.uiItemsListView.Location = new System.Drawing.Point(3, 34);
			this.uiItemsListView.MultiSelect = false;
			this.uiItemsListView.Name = "uiItemsListView";
			this.uiItemsListView.ShowItemToolTips = true;
			this.uiItemsListView.Size = new System.Drawing.Size(697, 465);
			this.uiItemsListView.TabIndex = 6;
			this.uiItemsListView.UseCompatibleStateImageBehavior = false;
			this.uiItemsListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.uiTexturesListView_ItemDrag);
			this.uiItemsListView.Click += new System.EventHandler(this.listView1Click);
			this.uiItemsListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.uiTexturesListView_DragDrop);
			this.uiItemsListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.uiItemsListView_DragEnter);
			this.uiItemsListView.DragOver += new System.Windows.Forms.DragEventHandler(this.uiTexturesListView_DragOver);
			this.uiItemsListView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.uiTexturesListView_GiveFeedback);
			this.uiItemsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.uiTexturesListView_MouseDoubleClick);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(477, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(60, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Choose...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.chooseFolderClick);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(439, 5);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 23);
			this.button2.TabIndex = 4;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.buttonFolderUpClick);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(684, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(0, 13);
			this.label4.TabIndex = 3;
			// 
			// uiThumbnailSizesCombo
			// 
			this.uiThumbnailSizesCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uiThumbnailSizesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uiThumbnailSizesCombo.FormattingEnabled = true;
			this.uiThumbnailSizesCombo.Location = new System.Drawing.Point(626, 7);
			this.uiThumbnailSizesCombo.Name = "uiThumbnailSizesCombo";
			this.uiThumbnailSizesCombo.Size = new System.Drawing.Size(74, 21);
			this.uiThumbnailSizesCombo.TabIndex = 2;
			this.uiThumbnailSizesCombo.SelectedIndexChanged += new System.EventHandler(this.comboSizeSelectedIndexChanged);
			// 
			// uiFolderText
			// 
			this.uiFolderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uiFolderText.Location = new System.Drawing.Point(45, 8);
			this.uiFolderText.Name = "uiFolderText";
			this.uiFolderText.ReadOnly = true;
			this.uiFolderText.Size = new System.Drawing.Size(388, 20);
			this.uiFolderText.TabIndex = 1;
			// 
			// fileSystemWatcher
			// 
			this.fileSystemWatcher.EnableRaisingEvents = true;
			this.fileSystemWatcher.SynchronizingObject = this;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// TexturePickerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.uiItemsListView);
			this.Controls.Add(this.uiFolderText);
			this.Controls.Add(this.uiThumbnailSizesCombo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Name = "TexturePickerControl";
			this.Size = new System.Drawing.Size(715, 502);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView uiItemsListView;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox uiThumbnailSizesCombo;
		private System.Windows.Forms.TextBox uiFolderText;
		private System.IO.FileSystemWatcher fileSystemWatcher;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
	}
}
