namespace Gleed2D.Core.Controls
{
	partial class LevelExplorerControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelExplorerControl));
			this.uiContextMenuForLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.LayerMoveUpEntry = new System.Windows.Forms.ToolStripMenuItem();
			this.LayerMoveDownEntry = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.uiContextMenuForLevel = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.centerViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.uiContextMenuForItem = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ItemCenterViewEntry = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.addCustomPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uiTreeToolsStrip = new System.Windows.Forms.ToolStrip();
			this.uiButtonNewLayer = new System.Windows.Forms.ToolStripButton();
			this.uiButtonDeleteLayer = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.uiButtonMoveLayerUp = new System.Windows.Forms.ToolStripButton();
			this.uiButtonMoveLayerDown = new System.Windows.Forms.ToolStripButton();
			this.uiTree = new System.Windows.Forms.TreeView();
			this.uiContextMenuForLayer.SuspendLayout();
			this.uiContextMenuForLevel.SuspendLayout();
			this.uiContextMenuForItem.SuspendLayout();
			this.uiTreeToolsStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiContextMenuForLayer
			// 
			this.uiContextMenuForLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem,
            this.toolStripSeparator11,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.LayerMoveUpEntry,
            this.LayerMoveDownEntry,
            this.toolStripSeparator18,
            this.toolStripMenuItem1});
			this.uiContextMenuForLayer.Name = "uiContextMenuForLayer";
			this.uiContextMenuForLayer.Size = new System.Drawing.Size(190, 176);
			// 
			// duplicateToolStripMenuItem
			// 
			this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			this.duplicateToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.duplicateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.duplicateToolStripMenuItem.Text = "Duplicate";
			this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(186, 6);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.actionRename);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.actionDelete);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
			// 
			// LayerMoveUpEntry
			// 
			this.LayerMoveUpEntry.Name = "LayerMoveUpEntry";
			this.LayerMoveUpEntry.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.LayerMoveUpEntry.Size = new System.Drawing.Size(189, 22);
			this.LayerMoveUpEntry.Text = "Move Up";
			this.LayerMoveUpEntry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LayerMoveUpEntry.Click += new System.EventHandler(this.actionMoveItemUp);
			// 
			// LayerMoveDownEntry
			// 
			this.LayerMoveDownEntry.Name = "LayerMoveDownEntry";
			this.LayerMoveDownEntry.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.LayerMoveDownEntry.Size = new System.Drawing.Size(189, 22);
			this.LayerMoveDownEntry.Text = "Move Down";
			this.LayerMoveDownEntry.Click += new System.EventHandler(this.actionMoveItemDown);
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(186, 6);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.toolStripMenuItem1.Text = "Add Custom Property";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.uiAddCustomPropertyClicked);
			// 
			// uiContextMenuForLevel
			// 
			this.uiContextMenuForLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerViewToolStripMenuItem,
            this.addLayerToolStripMenuItem,
            this.toolStripSeparator13,
            this.toolStripMenuItem2,
            this.toolStripSeparator15,
            this.toolStripMenuItem6});
			this.uiContextMenuForLevel.Name = "uiContextMenuForItem";
			this.uiContextMenuForLevel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.uiContextMenuForLevel.Size = new System.Drawing.Size(190, 104);
			// 
			// centerViewToolStripMenuItem
			// 
			this.centerViewToolStripMenuItem.Name = "centerViewToolStripMenuItem";
			this.centerViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.centerViewToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.centerViewToolStripMenuItem.Text = "Center View";
			this.centerViewToolStripMenuItem.Click += new System.EventHandler(this.actionCenterView);
			// 
			// addLayerToolStripMenuItem
			// 
			this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
			this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.addLayerToolStripMenuItem.Text = "Add Layer";
			this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.actionNewLayer);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(186, 6);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 22);
			this.toolStripMenuItem2.Text = "Rename";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.actionRename);
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(186, 6);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(189, 22);
			this.toolStripMenuItem6.Text = "Add Custom Property";
			this.toolStripMenuItem6.Click += new System.EventHandler(this.uiAddCustomPropertyClicked);
			// 
			// uiContextMenuForItem
			// 
			this.uiContextMenuForItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemCenterViewEntry,
            this.toolStripSeparator5,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.toolStripSeparator3,
            this.moveUpToolStripMenuItem1,
            this.moveDownToolStripMenuItem1,
            this.toolStripSeparator10,
            this.addCustomPropertyToolStripMenuItem});
			this.uiContextMenuForItem.Name = "uiContextMenuForItem";
			this.uiContextMenuForItem.Size = new System.Drawing.Size(190, 176);
			// 
			// ItemCenterViewEntry
			// 
			this.ItemCenterViewEntry.Name = "ItemCenterViewEntry";
			this.ItemCenterViewEntry.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.ItemCenterViewEntry.Size = new System.Drawing.Size(189, 22);
			this.ItemCenterViewEntry.Text = "Center View";
			this.ItemCenterViewEntry.Click += new System.EventHandler(this.actionCenterView);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(186, 6);
			// 
			// renameToolStripMenuItem1
			// 
			this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
			this.renameToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.renameToolStripMenuItem1.Text = "Rename";
			this.renameToolStripMenuItem1.Click += new System.EventHandler(this.actionRename);
			// 
			// deleteToolStripMenuItem1
			// 
			this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
			this.deleteToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.deleteToolStripMenuItem1.Text = "Delete";
			this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.actionDelete);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
			// 
			// moveUpToolStripMenuItem1
			// 
			this.moveUpToolStripMenuItem1.Name = "moveUpToolStripMenuItem1";
			this.moveUpToolStripMenuItem1.ShortcutKeyDisplayString = "";
			this.moveUpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.moveUpToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.moveUpToolStripMenuItem1.Text = "Move Up";
			this.moveUpToolStripMenuItem1.Click += new System.EventHandler(this.actionMoveItemUp);
			// 
			// moveDownToolStripMenuItem1
			// 
			this.moveDownToolStripMenuItem1.Name = "moveDownToolStripMenuItem1";
			this.moveDownToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.moveDownToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.moveDownToolStripMenuItem1.Text = "Move Down";
			this.moveDownToolStripMenuItem1.Click += new System.EventHandler(this.actionMoveItemDown);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(186, 6);
			// 
			// addCustomPropertyToolStripMenuItem
			// 
			this.addCustomPropertyToolStripMenuItem.Name = "addCustomPropertyToolStripMenuItem";
			this.addCustomPropertyToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.addCustomPropertyToolStripMenuItem.Text = "Add Custom Property";
			this.addCustomPropertyToolStripMenuItem.Click += new System.EventHandler(this.uiAddCustomPropertyClicked);
			// 
			// uiTreeToolsStrip
			// 
			this.uiTreeToolsStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.uiTreeToolsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiButtonNewLayer,
            this.uiButtonDeleteLayer,
            this.toolStripSeparator4,
            this.uiButtonMoveLayerUp,
            this.uiButtonMoveLayerDown});
			this.uiTreeToolsStrip.Location = new System.Drawing.Point(0, 0);
			this.uiTreeToolsStrip.Name = "uiTreeToolsStrip";
			this.uiTreeToolsStrip.Size = new System.Drawing.Size(285, 25);
			this.uiTreeToolsStrip.TabIndex = 3;
			// 
			// uiButtonNewLayer
			// 
			this.uiButtonNewLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.uiButtonNewLayer.Image = ((System.Drawing.Image)(resources.GetObject("uiButtonNewLayer.Image")));
			this.uiButtonNewLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uiButtonNewLayer.Name = "uiButtonNewLayer";
			this.uiButtonNewLayer.Size = new System.Drawing.Size(23, 22);
			this.uiButtonNewLayer.Text = "toolStripButton1";
			this.uiButtonNewLayer.ToolTipText = "New Layer (N)";
			this.uiButtonNewLayer.Click += new System.EventHandler(this.actionNewLayer);
			// 
			// uiButtonDeleteLayer
			// 
			this.uiButtonDeleteLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.uiButtonDeleteLayer.Image = ((System.Drawing.Image)(resources.GetObject("uiButtonDeleteLayer.Image")));
			this.uiButtonDeleteLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uiButtonDeleteLayer.Name = "uiButtonDeleteLayer";
			this.uiButtonDeleteLayer.Size = new System.Drawing.Size(23, 22);
			this.uiButtonDeleteLayer.Text = "Delete";
			this.uiButtonDeleteLayer.ToolTipText = "Delete (Del)";
			this.uiButtonDeleteLayer.Click += new System.EventHandler(this.actionDelete);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// uiButtonMoveLayerUp
			// 
			this.uiButtonMoveLayerUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.uiButtonMoveLayerUp.Image = ((System.Drawing.Image)(resources.GetObject("uiButtonMoveLayerUp.Image")));
			this.uiButtonMoveLayerUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uiButtonMoveLayerUp.Name = "uiButtonMoveLayerUp";
			this.uiButtonMoveLayerUp.Size = new System.Drawing.Size(23, 22);
			this.uiButtonMoveLayerUp.Text = "toolStripButton3";
			this.uiButtonMoveLayerUp.ToolTipText = "Move Up (F7)";
			this.uiButtonMoveLayerUp.Click += new System.EventHandler(this.actionMoveItemUp);
			// 
			// uiButtonMoveLayerDown
			// 
			this.uiButtonMoveLayerDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.uiButtonMoveLayerDown.Image = ((System.Drawing.Image)(resources.GetObject("uiButtonMoveLayerDown.Image")));
			this.uiButtonMoveLayerDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uiButtonMoveLayerDown.Name = "uiButtonMoveLayerDown";
			this.uiButtonMoveLayerDown.Size = new System.Drawing.Size(23, 22);
			this.uiButtonMoveLayerDown.Text = "toolStripButton4";
			this.uiButtonMoveLayerDown.ToolTipText = "Move Down (F8)";
			this.uiButtonMoveLayerDown.Click += new System.EventHandler(this.actionMoveItemDown);
			// 
			// uiTree
			// 
			this.uiTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiTree.Location = new System.Drawing.Point(0, 25);
			this.uiTree.Name = "uiTree";
			this.uiTree.Size = new System.Drawing.Size(285, 215);
			this.uiTree.TabIndex = 4;
			// 
			// LevelExplorerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.uiTree);
			this.Controls.Add(this.uiTreeToolsStrip);
			this.Name = "LevelExplorerControl";
			this.Size = new System.Drawing.Size(285, 240);
			this.uiContextMenuForLayer.ResumeLayout(false);
			this.uiContextMenuForLevel.ResumeLayout(false);
			this.uiContextMenuForItem.ResumeLayout(false);
			this.uiTreeToolsStrip.ResumeLayout(false);
			this.uiTreeToolsStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ContextMenuStrip uiContextMenuForLayer;
		private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem LayerMoveUpEntry;
		private System.Windows.Forms.ToolStripMenuItem LayerMoveDownEntry;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		public System.Windows.Forms.ContextMenuStrip uiContextMenuForLevel;
		private System.Windows.Forms.ToolStripMenuItem centerViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addLayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		public System.Windows.Forms.ContextMenuStrip uiContextMenuForItem;
		private System.Windows.Forms.ToolStripMenuItem ItemCenterViewEntry;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem addCustomPropertyToolStripMenuItem;
		private System.Windows.Forms.ToolStrip uiTreeToolsStrip;
		private System.Windows.Forms.ToolStripButton uiButtonNewLayer;
		private System.Windows.Forms.ToolStripButton uiButtonDeleteLayer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton uiButtonMoveLayerUp;
		private System.Windows.Forms.ToolStripButton uiButtonMoveLayerDown;
		private System.Windows.Forms.TreeView uiTree;
	}
}
