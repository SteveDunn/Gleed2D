namespace Gleed2D.Core.Controls
{
	partial class CustomPropertiesGridControl
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
			this.components = new System.ComponentModel.Container();
			this.CustomPropertyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteCustomPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CustomPropertyContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CustomPropertyContextMenu
			// 
			this.CustomPropertyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteCustomPropertyToolStripMenuItem});
			this.CustomPropertyContextMenu.Name = "CustomPropertyContextMenu";
			this.CustomPropertyContextMenu.Size = new System.Drawing.Size(201, 48);
			this.CustomPropertyContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CustomPropertyContextMenu_Opening);
			// 
			// deleteCustomPropertyToolStripMenuItem
			// 
			this.deleteCustomPropertyToolStripMenuItem.Name = "deleteCustomPropertyToolStripMenuItem";
			this.deleteCustomPropertyToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.deleteCustomPropertyToolStripMenuItem.Text = "Delete Custom Property";
			this.deleteCustomPropertyToolStripMenuItem.Click += new System.EventHandler(this.deleteCustomPropertyToolStripMenuItem_Click);
			// 
			// CustomPropertiesGridControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.CustomPropertyContextMenu;
			this.Name = "CustomPropertiesGridControl";
			this.CustomPropertyContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip CustomPropertyContextMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteCustomPropertyToolStripMenuItem;
	}
}
