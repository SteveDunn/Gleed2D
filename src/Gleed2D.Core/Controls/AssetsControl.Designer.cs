namespace Gleed2D.Core.Controls
{
	partial class AssetsControl
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uiTree = new System.Windows.Forms.TreeView();
			this.uiList = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uiTree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uiList);
			this.splitContainer1.Size = new System.Drawing.Size(409, 333);
			this.splitContainer1.SplitterDistance = 207;
			this.splitContainer1.TabIndex = 0;
			// 
			// uiTree
			// 
			this.uiTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiTree.Location = new System.Drawing.Point(0, 0);
			this.uiTree.Name = "uiTree";
			this.uiTree.Size = new System.Drawing.Size(207, 333);
			this.uiTree.TabIndex = 1;
			this.uiTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uiTree_AfterSelect);
			// 
			// uiList
			// 
			this.uiList.AllowDrop = true;
			this.uiList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiList.Location = new System.Drawing.Point(0, 0);
			this.uiList.Name = "uiList";
			this.uiList.Size = new System.Drawing.Size(198, 333);
			this.uiList.TabIndex = 0;
			this.uiList.UseCompatibleStateImageBehavior = false;
			this.uiList.View = System.Windows.Forms.View.List;
			this.uiList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.uiList_ItemDrag);
			this.uiList.DragDrop += new System.Windows.Forms.DragEventHandler(this.uiList_DragDrop);
			this.uiList.DragEnter += new System.Windows.Forms.DragEventHandler(this.uiList_DragEnter);
			this.uiList.DragOver += new System.Windows.Forms.DragEventHandler(this.uiList_DragOver);
			this.uiList.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.uiList_GiveFeedback);
			this.uiList.DoubleClick += new System.EventHandler(this.uiList_DoubleClick);
			// 
			// AssetsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "AssetsControl";
			this.Size = new System.Drawing.Size(409, 333);
			this.Load += new System.EventHandler(this.assetsControlLoad);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView uiTree;
		private System.Windows.Forms.ListView uiList;
	}
}
