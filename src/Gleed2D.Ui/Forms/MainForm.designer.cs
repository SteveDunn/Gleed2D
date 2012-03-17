using Gleed2D.Core.Controls ;

namespace GLEED2D.Forms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Rectangle", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Circle", 1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Path", 2);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("CustomRectangle", 0);
			this.uiCanvas = new System.Windows.Forms.PictureBox();
			this.uiEntityPropertyGrid = new Gleed2D.Core.Controls.CustomPropertiesGridControl();
			this.GridSite = new System.Windows.Forms.PropertyGrid();
			this.GridSite = new System.Windows.Forms.PropertyGrid();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.uiMoveSelectedItemsToLayerToolMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copySelectedItemsToLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.linkItemsByACustomPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewGrid = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewSnapToGrid = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewWorldOrigin = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.alignHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alignVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alignRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alignScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quickGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runLevelInYourOwnApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.undoButton = new System.Windows.Forms.ToolStripSplitButton();
			this.redoButton = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.uiZoomCombo = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.ShowGridButton = new System.Windows.Forms.ToolStripButton();
			this.SnapToGridButton = new System.Windows.Forms.ToolStripButton();
			this.ShowWorldOriginButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.uiMainPanel = new System.Windows.Forms.SplitContainer();
			this.uiSplitContainerExplorerAndPropertyGrid = new System.Windows.Forms.SplitContainer();
			this._uiLevelExplorer = new Gleed2D.Core.Controls.LevelExplorerControl();
			this.uiSplitContainerCanvasAndTabs = new System.Windows.Forms.SplitContainer();
			this.uiCategoriesTab = new System.Windows.Forms.TabControl();
			this.uiImageList = new System.Windows.Forms.ImageList(this.components);
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.listView3 = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.listView4 = new System.Windows.Forms.ListView();
			this.listView5 = new System.Windows.Forms.ListView();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.uiMenuStrip = new System.Windows.Forms.MenuStrip();
			this.behaviourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uiStartBehaviourMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uiStopBehaviourMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem24 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem25 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem26 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem27 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem28 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem29 = new System.Windows.Forms.ToolStripMenuItem();
			this.uiToolBar = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
			this.uiStatusBar = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uiAssetsControl = new Gleed2D.Core.Controls.AssetsControl();
			((System.ComponentModel.ISupportInitialize)(this.uiCanvas)).BeginInit();
			this.uiEntityPropertyGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiMainPanel)).BeginInit();
			this.uiMainPanel.Panel1.SuspendLayout();
			this.uiMainPanel.Panel2.SuspendLayout();
			this.uiMainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiSplitContainerExplorerAndPropertyGrid)).BeginInit();
			this.uiSplitContainerExplorerAndPropertyGrid.Panel1.SuspendLayout();
			this.uiSplitContainerExplorerAndPropertyGrid.Panel2.SuspendLayout();
			this.uiSplitContainerExplorerAndPropertyGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiSplitContainerCanvasAndTabs)).BeginInit();
			this.uiSplitContainerCanvasAndTabs.Panel1.SuspendLayout();
			this.uiSplitContainerCanvasAndTabs.Panel2.SuspendLayout();
			this.uiSplitContainerCanvasAndTabs.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.uiMenuStrip.SuspendLayout();
			this.uiToolBar.SuspendLayout();
			this.uiStatusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiCanvas
			// 
			this.uiCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiCanvas.Location = new System.Drawing.Point(0, 0);
			this.uiCanvas.Margin = new System.Windows.Forms.Padding(0);
			this.uiCanvas.Name = "uiCanvas";
			this.uiCanvas.Size = new System.Drawing.Size(581, 496);
			this.uiCanvas.TabIndex = 0;
			this.uiCanvas.TabStop = false;
			this.uiCanvas.DragDrop += new System.Windows.Forms.DragEventHandler(this.canvasDragDrop);
			this.uiCanvas.DragEnter += new System.Windows.Forms.DragEventHandler(this.canvasDragEnter);
			this.uiCanvas.DragOver += new System.Windows.Forms.DragEventHandler(this.canvasDragOver);
			this.uiCanvas.DragLeave += new System.EventHandler(this.canvasDragLeave);
			this.uiCanvas.MouseEnter += new System.EventHandler(this.canvasMouseEnter);
			this.uiCanvas.MouseLeave += new System.EventHandler(this.canvasMouseLeave);
			this.uiCanvas.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.canvasPreviewKeyDown);
			this.uiCanvas.Resize += new System.EventHandler(this.canvasResize);
			// 
			// uiEntityPropertyGrid
			// 
			// 
			// GridSite
			// 
			this.GridSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GridSite.Location = new System.Drawing.Point(0, 0);
			this.GridSite.Name = "GridSite";
			this.GridSite.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.GridSite.TabIndex = 0;
			this.GridSite.ToolbarVisible = false;
			this.GridSite.Visible = false;
			this.uiEntityPropertyGrid.Controls.Add(this.GridSite);
			this.uiEntityPropertyGrid.Controls.Add(this.GridSite);
			this.uiEntityPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiEntityPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.uiEntityPropertyGrid.Name = "uiEntityPropertyGrid";
			this.uiEntityPropertyGrid.Size = new System.Drawing.Size(239, 239);
			this.uiEntityPropertyGrid.TabIndex = 0;
			// 
			// GridSite
			// 
			this.GridSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GridSite.Location = new System.Drawing.Point(0, 0);
			this.GridSite.Name = "GridSite";
			this.GridSite.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.GridSite.Size = new System.Drawing.Size(239, 239);
			this.GridSite.TabIndex = 0;
			this.GridSite.ToolbarVisible = false;
			this.GridSite.Visible = false;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator5,
            this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.fileNewClicked);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.fileOpenClicked);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.fileSave);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.onSaveAsClicked);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(269, 6);
			// 
			// uiImportAnOlderVersionOfAGleedFileToolStripMenuItem
			// 
			this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem.Name = "uiImportAnOlderVersionOfAGleedFileToolStripMenuItem";
			this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem.Text = "&Import an older version of a Gleed file";
			this.uiImportAnOlderVersionOfAGleedFileToolStripMenuItem.Click += new System.EventHandler(this.whenImportOfOldGleedLevelIsClicked);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(269, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.onCloseClicked);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator9,
            this.uiMoveSelectedItemsToLayerToolMenu,
            this.copySelectedItemsToLayerToolStripMenuItem,
            this.toolStripSeparator2,
            this.linkItemsByACustomPropertyToolStripMenuItem,
            this.toolStripSeparator3,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoMenuItem
			// 
			this.undoMenuItem.Enabled = false;
			this.undoMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoMenuItem.Image")));
			this.undoMenuItem.Name = "undoMenuItem";
			this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoMenuItem.Size = new System.Drawing.Size(362, 22);
			this.undoMenuItem.Text = "Undo";
			this.undoMenuItem.Click += new System.EventHandler(this.editUndo);
			// 
			// redoMenuItem
			// 
			this.redoMenuItem.Enabled = false;
			this.redoMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoMenuItem.Image")));
			this.redoMenuItem.Name = "redoMenuItem";
			this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoMenuItem.Size = new System.Drawing.Size(362, 22);
			this.redoMenuItem.Text = "Redo";
			this.redoMenuItem.Click += new System.EventHandler(this.editRedo);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(359, 6);
			// 
			// uiMoveSelectedItemsToLayerToolMenu
			// 
			this.uiMoveSelectedItemsToLayerToolMenu.Image = ((System.Drawing.Image)(resources.GetObject("uiMoveSelectedItemsToLayerToolMenu.Image")));
			this.uiMoveSelectedItemsToLayerToolMenu.Name = "uiMoveSelectedItemsToLayerToolMenu";
			this.uiMoveSelectedItemsToLayerToolMenu.Size = new System.Drawing.Size(362, 22);
			this.uiMoveSelectedItemsToLayerToolMenu.Text = "Move Selected Items to Layer...";
			this.uiMoveSelectedItemsToLayerToolMenu.ToolTipText = "Moves the selected Items to another existing layer.";
			this.uiMoveSelectedItemsToLayerToolMenu.DropDownOpening += new System.EventHandler(this.moveSelectedItemsToLayerMenuOpening);
			// 
			// copySelectedItemsToLayerToolStripMenuItem
			// 
			this.copySelectedItemsToLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copySelectedItemsToLayerToolStripMenuItem.Image")));
			this.copySelectedItemsToLayerToolStripMenuItem.Name = "copySelectedItemsToLayerToolStripMenuItem";
			this.copySelectedItemsToLayerToolStripMenuItem.Size = new System.Drawing.Size(362, 22);
			this.copySelectedItemsToLayerToolStripMenuItem.Text = "Copy Selected Items to Layer...";
			this.copySelectedItemsToLayerToolStripMenuItem.ToolTipText = "Copies the selected Items to another existing layer.";
			this.copySelectedItemsToLayerToolStripMenuItem.DropDownOpening += new System.EventHandler(this.copySelectedItemsToLayerMenuOpening);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(359, 6);
			// 
			// linkItemsByACustomPropertyToolStripMenuItem
			// 
			this.linkItemsByACustomPropertyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("linkItemsByACustomPropertyToolStripMenuItem.Image")));
			this.linkItemsByACustomPropertyToolStripMenuItem.Name = "linkItemsByACustomPropertyToolStripMenuItem";
			this.linkItemsByACustomPropertyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
			this.linkItemsByACustomPropertyToolStripMenuItem.Size = new System.Drawing.Size(362, 22);
			this.linkItemsByACustomPropertyToolStripMenuItem.Text = "Link Selected Items by a CustomProperty";
			this.linkItemsByACustomPropertyToolStripMenuItem.ToolTipText = "Only active if exactly two Items are selected. Adds a Custom Property of type \"It" +
    "em\" resulting in the first Item referring to the Second one. \r\nTwo-way linking a" +
    "lso possible.";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(359, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(362, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.editSelectAll);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewGrid,
            this.ViewSnapToGrid,
            this.ViewWorldOrigin});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// ViewGrid
			// 
			this.ViewGrid.CheckOnClick = true;
			this.ViewGrid.Image = ((System.Drawing.Image)(resources.GetObject("ViewGrid.Image")));
			this.ViewGrid.Name = "ViewGrid";
			this.ViewGrid.Size = new System.Drawing.Size(142, 22);
			this.ViewGrid.Text = "Grid";
			this.ViewGrid.CheckedChanged += new System.EventHandler(this.viewGridCheckedChanged);
			// 
			// ViewSnapToGrid
			// 
			this.ViewSnapToGrid.CheckOnClick = true;
			this.ViewSnapToGrid.Image = ((System.Drawing.Image)(resources.GetObject("ViewSnapToGrid.Image")));
			this.ViewSnapToGrid.Name = "ViewSnapToGrid";
			this.ViewSnapToGrid.Size = new System.Drawing.Size(142, 22);
			this.ViewSnapToGrid.Text = "Snap to Grid";
			this.ViewSnapToGrid.CheckedChanged += new System.EventHandler(this.viewSnapToGridCheckedChanged);
			// 
			// ViewWorldOrigin
			// 
			this.ViewWorldOrigin.CheckOnClick = true;
			this.ViewWorldOrigin.Image = ((System.Drawing.Image)(resources.GetObject("ViewWorldOrigin.Image")));
			this.ViewWorldOrigin.Name = "ViewWorldOrigin";
			this.ViewWorldOrigin.Size = new System.Drawing.Size(142, 22);
			this.ViewWorldOrigin.Text = "World Origin";
			this.ViewWorldOrigin.CheckedChanged += new System.EventHandler(this.viewWorldOriginCheckedChanged);
			// 
			// ToolsMenu
			// 
			this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alignHorizontallyToolStripMenuItem,
            this.alignVerticallyToolStripMenuItem,
            this.alignRotationToolStripMenuItem,
            this.alignScaleToolStripMenuItem});
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new System.Drawing.Size(57, 20);
			this.ToolsMenu.Text = "&Format";
			this.ToolsMenu.Click += new System.EventHandler(this.toolsMenuClick);
			this.ToolsMenu.MouseEnter += new System.EventHandler(this.toolsMenuMouseEnter);
			// 
			// alignHorizontallyToolStripMenuItem
			// 
			this.alignHorizontallyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignHorizontallyToolStripMenuItem.Image")));
			this.alignHorizontallyToolStripMenuItem.Name = "alignHorizontallyToolStripMenuItem";
			this.alignHorizontallyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
			this.alignHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
			this.alignHorizontallyToolStripMenuItem.Text = "Align Horizontally";
			this.alignHorizontallyToolStripMenuItem.ToolTipText = "Adjust the Y-Coordinate of all selected items to be the same as the first selecte" +
    "d item.";
			this.alignHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.toolsAlignHorizontally);
			// 
			// alignVerticallyToolStripMenuItem
			// 
			this.alignVerticallyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignVerticallyToolStripMenuItem.Image")));
			this.alignVerticallyToolStripMenuItem.Name = "alignVerticallyToolStripMenuItem";
			this.alignVerticallyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
			this.alignVerticallyToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
			this.alignVerticallyToolStripMenuItem.Text = "Align Vertically";
			this.alignVerticallyToolStripMenuItem.ToolTipText = "Adjust the X-Coordinate of all selected items to be the same as the first selecte" +
    "d item.";
			this.alignVerticallyToolStripMenuItem.Click += new System.EventHandler(this.toolsAlignVertically);
			// 
			// alignRotationToolStripMenuItem
			// 
			this.alignRotationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignRotationToolStripMenuItem.Image")));
			this.alignRotationToolStripMenuItem.Name = "alignRotationToolStripMenuItem";
			this.alignRotationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
			this.alignRotationToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
			this.alignRotationToolStripMenuItem.Text = "Align Rotation";
			this.alignRotationToolStripMenuItem.ToolTipText = "Adjust the rotation of all selected items to be the same as the first selected it" +
    "em.";
			this.alignRotationToolStripMenuItem.Click += new System.EventHandler(this.toolsAlignRotation);
			// 
			// alignScaleToolStripMenuItem
			// 
			this.alignScaleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignScaleToolStripMenuItem.Image")));
			this.alignScaleToolStripMenuItem.Name = "alignScaleToolStripMenuItem";
			this.alignScaleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.alignScaleToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
			this.alignScaleToolStripMenuItem.Text = "Align Scale";
			this.alignScaleToolStripMenuItem.ToolTipText = "Adjust the Scale of all selected items to be the same as the first selected item." +
    "";
			this.alignScaleToolStripMenuItem.Click += new System.EventHandler(this.toolsAlignScale);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickGuideToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// quickGuideToolStripMenuItem
			// 
			this.quickGuideToolStripMenuItem.Name = "quickGuideToolStripMenuItem";
			this.quickGuideToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.quickGuideToolStripMenuItem.Text = "QuickGuide";
			this.quickGuideToolStripMenuItem.Click += new System.EventHandler(this.helpQuickGuide);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.helpAbout);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runLevelInYourOwnApplicationToolStripMenuItem,
            this.toolStripSeparator4,
            this.settingsToolStripMenuItem1});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// runLevelInYourOwnApplicationToolStripMenuItem
			// 
			this.runLevelInYourOwnApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runLevelInYourOwnApplicationToolStripMenuItem.Image")));
			this.runLevelInYourOwnApplicationToolStripMenuItem.Name = "runLevelInYourOwnApplicationToolStripMenuItem";
			this.runLevelInYourOwnApplicationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.runLevelInYourOwnApplicationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.runLevelInYourOwnApplicationToolStripMenuItem.Text = "Run Level";
			this.runLevelInYourOwnApplicationToolStripMenuItem.ToolTipText = "Run Level in your own application. To use this, define the appropriate settings i" +
    "n the Tools->Settings dialog.";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
			// 
			// settingsToolStripMenuItem1
			// 
			this.settingsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem1.Image")));
			this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
			this.settingsToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
			this.settingsToolStripMenuItem1.Text = "Settings";
			this.settingsToolStripMenuItem1.ToolTipText = "The Settings Dialog.";
			this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.showToolsSettingsForm);
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			this.newToolStripButton.Click += new System.EventHandler(this.fileNewClicked);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.Click += new System.EventHandler(this.fileOpenClicked);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.ToolTipText = "Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.fileSave);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// undoButton
			// 
			this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
			this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(32, 22);
			this.undoButton.Text = "toolStripButton2";
			this.undoButton.ToolTipText = "Undo";
			this.undoButton.ButtonClick += new System.EventHandler(this.editUndo);
			this.undoButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.undoManyCommands);
			// 
			// redoButton
			// 
			this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.redoButton.Enabled = false;
			this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
			this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoButton.Name = "redoButton";
			this.redoButton.Size = new System.Drawing.Size(32, 22);
			this.redoButton.Text = "toolStripButton3";
			this.redoButton.ToolTipText = "Redo";
			this.redoButton.ButtonClick += new System.EventHandler(this.editRedo);
			this.redoButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.redoManyCommands);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
			this.toolStripLabel1.Text = "Zoom:";
			// 
			// uiZoomCombo
			// 
			this.uiZoomCombo.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.uiZoomCombo.Name = "uiZoomCombo";
			this.uiZoomCombo.Size = new System.Drawing.Size(75, 25);
			this.uiZoomCombo.TextChanged += new System.EventHandler(this.zoomComboTextChanged);
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
			// 
			// ShowGridButton
			// 
			this.ShowGridButton.CheckOnClick = true;
			this.ShowGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ShowGridButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowGridButton.Image")));
			this.ShowGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShowGridButton.Name = "ShowGridButton";
			this.ShowGridButton.Size = new System.Drawing.Size(23, 22);
			this.ShowGridButton.Text = "toolStripButton4";
			this.ShowGridButton.ToolTipText = "Show Grid";
			this.ShowGridButton.CheckedChanged += new System.EventHandler(this.showGridButtonCheckedChanged);
			// 
			// SnapToGridButton
			// 
			this.SnapToGridButton.CheckOnClick = true;
			this.SnapToGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SnapToGridButton.Image = ((System.Drawing.Image)(resources.GetObject("SnapToGridButton.Image")));
			this.SnapToGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SnapToGridButton.Name = "SnapToGridButton";
			this.SnapToGridButton.Size = new System.Drawing.Size(23, 22);
			this.SnapToGridButton.Text = "toolStripButton4";
			this.SnapToGridButton.ToolTipText = "Snap to Grid";
			this.SnapToGridButton.CheckedChanged += new System.EventHandler(this.snapToGridButtonCheckedChanged);
			// 
			// ShowWorldOriginButton
			// 
			this.ShowWorldOriginButton.CheckOnClick = true;
			this.ShowWorldOriginButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ShowWorldOriginButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowWorldOriginButton.Image")));
			this.ShowWorldOriginButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShowWorldOriginButton.Name = "ShowWorldOriginButton";
			this.ShowWorldOriginButton.Size = new System.Drawing.Size(23, 22);
			this.ShowWorldOriginButton.Text = "toolStripButton4";
			this.ShowWorldOriginButton.ToolTipText = "Show World Origin";
			this.ShowWorldOriginButton.CheckedChanged += new System.EventHandler(this.showWorldOriginButtonCheckedChanged);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "Run Level";
			this.toolStripButton2.ToolTipText = "Run Level in your own application. To use this, define the appropriate settings i" +
    "n the Tools->Settings dialog.";
			this.toolStripButton2.Click += new System.EventHandler(this.runLevel);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "Settings";
			this.toolStripButton3.Click += new System.EventHandler(this.showToolsSettingsForm);
			// 
			// uiMainPanel
			// 
			this.uiMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.uiMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiMainPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.uiMainPanel.Location = new System.Drawing.Point(0, 0);
			this.uiMainPanel.Name = "uiMainPanel";
			// 
			// uiMainPanel.Panel1
			// 
			this.uiMainPanel.Panel1.Controls.Add(this.uiSplitContainerExplorerAndPropertyGrid);
			// 
			// uiMainPanel.Panel2
			// 
			this.uiMainPanel.Panel2.Controls.Add(this.uiSplitContainerCanvasAndTabs);
			this.uiMainPanel.Size = new System.Drawing.Size(832, 713);
			this.uiMainPanel.SplitterDistance = 243;
			this.uiMainPanel.TabIndex = 6;
			// 
			// uiSplitContainerExplorerAndPropertyGrid
			// 
			this.uiSplitContainerExplorerAndPropertyGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.uiSplitContainerExplorerAndPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiSplitContainerExplorerAndPropertyGrid.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.uiSplitContainerExplorerAndPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.uiSplitContainerExplorerAndPropertyGrid.Name = "uiSplitContainerExplorerAndPropertyGrid";
			this.uiSplitContainerExplorerAndPropertyGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// uiSplitContainerExplorerAndPropertyGrid.Panel1
			// 
			this.uiSplitContainerExplorerAndPropertyGrid.Panel1.Controls.Add(this._uiLevelExplorer);
			// 
			// uiSplitContainerExplorerAndPropertyGrid.Panel2
			// 
			this.uiSplitContainerExplorerAndPropertyGrid.Panel2.Controls.Add(this.uiEntityPropertyGrid);
			this.uiSplitContainerExplorerAndPropertyGrid.Size = new System.Drawing.Size(243, 713);
			this.uiSplitContainerExplorerAndPropertyGrid.SplitterDistance = 466;
			this.uiSplitContainerExplorerAndPropertyGrid.TabIndex = 0;
			// 
			// _uiLevelExplorer
			// 
			this._uiLevelExplorer.CheckBoxes = true;
			this._uiLevelExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiLevelExplorer.HideContextMenus = false;
			this._uiLevelExplorer.InteractsWithModel = true;
			this._uiLevelExplorer.Location = new System.Drawing.Point(0, 0);
			this._uiLevelExplorer.Name = "_uiLevelExplorer";
			this._uiLevelExplorer.ShowToolStrip = true;
			this._uiLevelExplorer.Size = new System.Drawing.Size(239, 462);
			this._uiLevelExplorer.TabIndex = 2;
			// 
			// uiSplitContainerCanvasAndTabs
			// 
			this.uiSplitContainerCanvasAndTabs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.uiSplitContainerCanvasAndTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiSplitContainerCanvasAndTabs.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.uiSplitContainerCanvasAndTabs.Location = new System.Drawing.Point(0, 0);
			this.uiSplitContainerCanvasAndTabs.Name = "uiSplitContainerCanvasAndTabs";
			this.uiSplitContainerCanvasAndTabs.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// uiSplitContainerCanvasAndTabs.Panel1
			// 
			this.uiSplitContainerCanvasAndTabs.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.uiSplitContainerCanvasAndTabs.Panel1.Controls.Add(this.uiCanvas);
			// 
			// uiSplitContainerCanvasAndTabs.Panel2
			// 
			this.uiSplitContainerCanvasAndTabs.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.uiSplitContainerCanvasAndTabs.Panel2.Controls.Add(this.uiCategoriesTab);
			this.uiSplitContainerCanvasAndTabs.Size = new System.Drawing.Size(585, 713);
			this.uiSplitContainerCanvasAndTabs.SplitterDistance = 500;
			this.uiSplitContainerCanvasAndTabs.TabIndex = 0;
			// 
			// uiCategoriesTab
			// 
			this.uiCategoriesTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiCategoriesTab.HotTrack = true;
			this.uiCategoriesTab.Location = new System.Drawing.Point(0, 0);
			this.uiCategoriesTab.Name = "uiCategoriesTab";
			this.uiCategoriesTab.SelectedIndex = 0;
			this.uiCategoriesTab.Size = new System.Drawing.Size(581, 205);
			this.uiCategoriesTab.TabIndex = 0;
			// 
			// uiImageList
			// 
			this.uiImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.uiImageList.ImageSize = new System.Drawing.Size(256, 256);
			this.uiImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.listView3);
			this.tabPage3.Controls.Add(this.button1);
			this.tabPage3.Controls.Add(this.button2);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.comboBox2);
			this.tabPage3.Controls.Add(this.textBox2);
			this.tabPage3.Controls.Add(this.label4);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(743, 167);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Textures";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// listView3
			// 
			this.listView3.AllowDrop = true;
			this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView3.HideSelection = false;
			this.listView3.Location = new System.Drawing.Point(9, 34);
			this.listView3.MultiSelect = false;
			this.listView3.Name = "listView3";
			this.listView3.ShowItemToolTips = true;
			this.listView3.Size = new System.Drawing.Size(752, 127);
			this.listView3.TabIndex = 6;
			this.listView3.UseCompatibleStateImageBehavior = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(549, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(60, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Choose...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(511, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 23);
			this.button2.TabIndex = 4;
			this.button2.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(657, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Size:";
			// 
			// comboBox2
			// 
			this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(687, 8);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(74, 21);
			this.comboBox2.TabIndex = 2;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(51, 8);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(454, 20);
			this.textBox2.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Folder:";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.listView4);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(743, 167);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Primitives";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// listView4
			// 
			this.listView4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView4.HideSelection = false;
			listViewItem1.ToolTipText = "A simple rectangle defined by position, width and height. Rectangle Primitives ca" +
    "n be scaled but not rotated.";
			listViewItem2.ToolTipText = "A simple circle defined by position and radius. Circle Primitives can be scaled b" +
    "ut not rotated.";
			listViewItem3.ToolTipText = "A Path is an array of Vector2. Path Primitives can be rotated and scaled.";
			this.listView4.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
			this.listView4.Location = new System.Drawing.Point(3, 3);
			this.listView4.MultiSelect = false;
			this.listView4.Name = "listView4";
			this.listView4.ShowItemToolTips = true;
			this.listView4.Size = new System.Drawing.Size(737, 161);
			this.listView4.TabIndex = 0;
			this.listView4.UseCompatibleStateImageBehavior = false;
			// 
			// listView5
			// 
			this.listView5.AllowDrop = true;
			this.listView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView5.HideSelection = false;
			this.listView5.Location = new System.Drawing.Point(9, 34);
			this.listView5.MultiSelect = false;
			this.listView5.Name = "listView5";
			this.listView5.ShowItemToolTips = true;
			this.listView5.Size = new System.Drawing.Size(788, 127);
			this.listView5.TabIndex = 6;
			this.listView5.UseCompatibleStateImageBehavior = false;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(585, 6);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(60, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "Choose...";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
			this.button4.Location = new System.Drawing.Point(547, 6);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(32, 23);
			this.button4.TabIndex = 4;
			this.button4.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(693, 11);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Size:";
			// 
			// comboBox3
			// 
			this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(723, 8);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(74, 21);
			this.comboBox3.TabIndex = 2;
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Location = new System.Drawing.Point(51, 8);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(490, 20);
			this.textBox3.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Folder:";
			// 
			// uiMenuStrip
			// 
			this.uiMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.ToolsMenu,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.behaviourToolStripMenuItem});
			this.uiMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.uiMenuStrip.Name = "uiMenuStrip";
			this.uiMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.uiMenuStrip.Size = new System.Drawing.Size(1228, 24);
			this.uiMenuStrip.TabIndex = 4;
			this.uiMenuStrip.Text = "menuStrip1";
			// 
			// behaviourToolStripMenuItem
			// 
			this.behaviourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiStartBehaviourMenuItem,
            this.uiStopBehaviourMenuItem});
			this.behaviourToolStripMenuItem.Name = "behaviourToolStripMenuItem";
			this.behaviourToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
			this.behaviourToolStripMenuItem.Text = "Behaviour";
			// 
			// uiStartBehaviourMenuItem
			// 
			this.uiStartBehaviourMenuItem.Name = "uiStartBehaviourMenuItem";
			this.uiStartBehaviourMenuItem.Size = new System.Drawing.Size(98, 22);
			this.uiStartBehaviourMenuItem.Text = "Start";
			this.uiStartBehaviourMenuItem.Click += new System.EventHandler(this.uiStartBehaviourMenuItem_Click);
			// 
			// uiStopBehaviourMenuItem
			// 
			this.uiStopBehaviourMenuItem.Name = "uiStopBehaviourMenuItem";
			this.uiStopBehaviourMenuItem.Size = new System.Drawing.Size(98, 22);
			this.uiStopBehaviourMenuItem.Text = "Stop";
			this.uiStopBehaviourMenuItem.Click += new System.EventHandler(this.uiStopBehaviourMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			this.toolStripMenuItem12.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			this.toolStripMenuItem13.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem14
			// 
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			this.toolStripMenuItem14.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem15
			// 
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			this.toolStripMenuItem15.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem16
			// 
			this.toolStripMenuItem16.Name = "toolStripMenuItem16";
			this.toolStripMenuItem16.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem17
			// 
			this.toolStripMenuItem17.Name = "toolStripMenuItem17";
			this.toolStripMenuItem17.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem18
			// 
			this.toolStripMenuItem18.Name = "toolStripMenuItem18";
			this.toolStripMenuItem18.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem19
			// 
			this.toolStripMenuItem19.Name = "toolStripMenuItem19";
			this.toolStripMenuItem19.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem20
			// 
			this.toolStripMenuItem20.Name = "toolStripMenuItem20";
			this.toolStripMenuItem20.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem21
			// 
			this.toolStripMenuItem21.Name = "toolStripMenuItem21";
			this.toolStripMenuItem21.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem22
			// 
			this.toolStripMenuItem22.Name = "toolStripMenuItem22";
			this.toolStripMenuItem22.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem23
			// 
			this.toolStripMenuItem23.Name = "toolStripMenuItem23";
			this.toolStripMenuItem23.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem24
			// 
			this.toolStripMenuItem24.Name = "toolStripMenuItem24";
			this.toolStripMenuItem24.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem25
			// 
			this.toolStripMenuItem25.Name = "toolStripMenuItem25";
			this.toolStripMenuItem25.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem26
			// 
			this.toolStripMenuItem26.Name = "toolStripMenuItem26";
			this.toolStripMenuItem26.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem27
			// 
			this.toolStripMenuItem27.Name = "toolStripMenuItem27";
			this.toolStripMenuItem27.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripMenuItem28
			// 
			this.toolStripMenuItem28.Name = "toolStripMenuItem28";
			this.toolStripMenuItem28.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripMenuItem29
			// 
			this.toolStripMenuItem29.Name = "toolStripMenuItem29";
			this.toolStripMenuItem29.Size = new System.Drawing.Size(32, 19);
			// 
			// uiToolBar
			// 
			this.uiToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.uiToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator8,
            this.toolStripLabel1,
            this.uiZoomCombo,
            this.toolStripSeparator17,
            this.ShowGridButton,
            this.SnapToGridButton,
            this.ShowWorldOriginButton,
            this.toolStripSeparator12,
            this.toolStripButton2,
            this.toolStripSeparator14,
            this.toolStripButton3});
			this.uiToolBar.Location = new System.Drawing.Point(0, 24);
			this.uiToolBar.Name = "uiToolBar";
			this.uiToolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.uiToolBar.Size = new System.Drawing.Size(1228, 25);
			this.uiToolBar.Stretch = true;
			this.uiToolBar.TabIndex = 5;
			this.uiToolBar.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSplitButton2
			// 
			this.toolStripSplitButton2.Name = "toolStripSplitButton2";
			this.toolStripSplitButton2.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
			// 
			// toolStripSeparator19
			// 
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripButton6
			// 
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripButton7
			// 
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripButton8
			// 
			this.toolStripButton8.Name = "toolStripButton8";
			this.toolStripButton8.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator20
			// 
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripButton9
			// 
			this.toolStripButton9.Name = "toolStripButton9";
			this.toolStripButton9.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator21
			// 
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(6, 6);
			// 
			// toolStripButton10
			// 
			this.toolStripButton10.Name = "toolStripButton10";
			this.toolStripButton10.Size = new System.Drawing.Size(23, 23);
			// 
			// uiStatusBar
			// 
			this.uiStatusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.uiStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel8});
			this.uiStatusBar.Location = new System.Drawing.Point(0, 762);
			this.uiStatusBar.Name = "uiStatusBar";
			this.uiStatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.uiStatusBar.Size = new System.Drawing.Size(1228, 22);
			this.uiStatusBar.TabIndex = 7;
			this.uiStatusBar.Text = "statusStrip1";
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.AutoSize = false;
			this.toolStripStatusLabel5.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel5.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(650, 17);
			this.toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel6
			// 
			this.toolStripStatusLabel6.AutoSize = false;
			this.toolStripStatusLabel6.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
			this.toolStripStatusLabel6.Size = new System.Drawing.Size(113, 17);
			this.toolStripStatusLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel7
			// 
			this.toolStripStatusLabel7.AutoSize = false;
			this.toolStripStatusLabel7.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel7.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
			this.toolStripStatusLabel7.Size = new System.Drawing.Size(109, 17);
			this.toolStripStatusLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel8
			// 
			this.toolStripStatusLabel8.AutoSize = false;
			this.toolStripStatusLabel8.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel8.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
			this.toolStripStatusLabel8.Size = new System.Drawing.Size(60, 17);
			this.toolStripStatusLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 49);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uiMainPanel);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uiAssetsControl);
			this.splitContainer1.Size = new System.Drawing.Size(1228, 713);
			this.splitContainer1.SplitterDistance = 832;
			this.splitContainer1.TabIndex = 1;
			// 
			// uiAssetsControl
			// 
			this.uiAssetsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiAssetsControl.Location = new System.Drawing.Point(0, 0);
			this.uiAssetsControl.Name = "uiAssetsControl";
			this.uiAssetsControl.Size = new System.Drawing.Size(392, 713);
			this.uiAssetsControl.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1228, 784);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.uiStatusBar);
			this.Controls.Add(this.uiToolBar);
			this.Controls.Add(this.uiMenuStrip);
			this.Location = new System.Drawing.Point(10, 10);
			this.Name = "MainForm";
			this.Text = "GLEED2D";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFormFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainFormFormClosed);
			this.Load += new System.EventHandler(this.mainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.uiCanvas)).EndInit();
			this.uiEntityPropertyGrid.ResumeLayout(false);
			this.uiMainPanel.Panel1.ResumeLayout(false);
			this.uiMainPanel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiMainPanel)).EndInit();
			this.uiMainPanel.ResumeLayout(false);
			this.uiSplitContainerExplorerAndPropertyGrid.Panel1.ResumeLayout(false);
			this.uiSplitContainerExplorerAndPropertyGrid.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiSplitContainerExplorerAndPropertyGrid)).EndInit();
			this.uiSplitContainerExplorerAndPropertyGrid.ResumeLayout(false);
			this.uiSplitContainerCanvasAndTabs.Panel1.ResumeLayout(false);
			this.uiSplitContainerCanvasAndTabs.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiSplitContainerCanvasAndTabs)).EndInit();
			this.uiSplitContainerCanvasAndTabs.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.uiMenuStrip.ResumeLayout(false);
			this.uiMenuStrip.PerformLayout();
			this.uiToolBar.ResumeLayout(false);
			this.uiToolBar.PerformLayout();
			this.uiStatusBar.ResumeLayout(false);
			this.uiStatusBar.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public Gleed2D.Core.Controls.CustomPropertiesGridControl uiEntityPropertyGrid;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		public System.Windows.Forms.PictureBox uiCanvas;
        private System.Windows.Forms.SplitContainer uiMainPanel;
        private System.Windows.Forms.SplitContainer uiSplitContainerExplorerAndPropertyGrid;
        private System.Windows.Forms.SplitContainer uiSplitContainerCanvasAndTabs;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripComboBox uiZoomCombo;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public System.Windows.Forms.ContextMenuStrip ItemContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ToolsMenu;
        private System.Windows.Forms.ToolStripMenuItem alignHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignRotationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem alignScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        public System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        public System.Windows.Forms.ToolStripMenuItem redoMenuItem;
        public System.Windows.Forms.ToolStripSplitButton undoButton;
        public System.Windows.Forms.ToolStripSplitButton redoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ImageList uiImageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ViewGrid;
		public System.Windows.Forms.ToolStripMenuItem ViewWorldOrigin;
        private System.Windows.Forms.ToolStripButton ShowGridButton;
		private System.Windows.Forms.ToolStripButton ShowWorldOriginButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem quickGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SnapToGridButton;
		public System.Windows.Forms.ToolStripMenuItem ViewSnapToGrid;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage4;
		public System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolStripMenuItem uiImportAnOlderVersionOfAGleedFileToolStripMenuItem;
		private LevelExplorerControl _uiLevelExplorer;
		private System.Windows.Forms.ToolStripMenuItem uiMoveSelectedItemsToLayerToolMenu;
		private System.Windows.Forms.ToolStripMenuItem copySelectedItemsToLayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem linkItemsByACustomPropertyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runLevelInYourOwnApplicationToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
		private System.Windows.Forms.TabControl uiCategoriesTab;
		private System.Windows.Forms.MenuStrip uiMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem26;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;
		private System.Windows.Forms.ToolStrip uiToolBar;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripButton toolStripButton5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		public System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		public System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		public System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
		private System.Windows.Forms.ToolStripButton toolStripButton6;
		private System.Windows.Forms.ToolStripButton toolStripButton7;
		private System.Windows.Forms.ToolStripButton toolStripButton8;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
		private System.Windows.Forms.ToolStripButton toolStripButton9;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
		private System.Windows.Forms.ToolStripButton toolStripButton10;
		private System.Windows.Forms.StatusStrip uiStatusBar;
		public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
		public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
		public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
		private System.Windows.Forms.PropertyGrid GridSite;
		private System.Windows.Forms.ToolStripMenuItem behaviourToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uiStartBehaviourMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uiStopBehaviourMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private AssetsControl uiAssetsControl;
    }
}