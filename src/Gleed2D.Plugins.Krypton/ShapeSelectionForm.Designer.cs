namespace Gleed2D.Plugins.Krypton
{
	partial class ShapeSelectionForm
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
			this.uiApplyButton = new System.Windows.Forms.Button();
			this.uiCancelButton = new System.Windows.Forms.Button();
			this.uiShapeList = new System.Windows.Forms.ListView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uiShapePropertyGrid = new System.Windows.Forms.PropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiApplyButton
			// 
			this.uiApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uiApplyButton.Location = new System.Drawing.Point(401, 399);
			this.uiApplyButton.Name = "uiApplyButton";
			this.uiApplyButton.Size = new System.Drawing.Size(75, 23);
			this.uiApplyButton.TabIndex = 1;
			this.uiApplyButton.Text = "&Apply";
			this.uiApplyButton.UseVisualStyleBackColor = true;
			this.uiApplyButton.Click += new System.EventHandler(this.uiApplyButton_Click);
			// 
			// uiCancelButton
			// 
			this.uiCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uiCancelButton.Location = new System.Drawing.Point(482, 399);
			this.uiCancelButton.Name = "uiCancelButton";
			this.uiCancelButton.Size = new System.Drawing.Size(75, 23);
			this.uiCancelButton.TabIndex = 1;
			this.uiCancelButton.Text = "Cancel";
			this.uiCancelButton.UseVisualStyleBackColor = true;
			this.uiCancelButton.Click += new System.EventHandler(this.uiCancelButton_Click);
			// 
			// uiShapeList
			// 
			this.uiShapeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiShapeList.Location = new System.Drawing.Point(0, 0);
			this.uiShapeList.Name = "uiShapeList";
			this.uiShapeList.Size = new System.Drawing.Size(181, 381);
			this.uiShapeList.TabIndex = 2;
			this.uiShapeList.UseCompatibleStateImageBehavior = false;
			this.uiShapeList.SelectedIndexChanged += new System.EventHandler(this.uiShapeList_SelectedIndexChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uiShapeList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uiShapePropertyGrid);
			this.splitContainer1.Size = new System.Drawing.Size(545, 381);
			this.splitContainer1.SplitterDistance = 181;
			this.splitContainer1.TabIndex = 3;
			// 
			// uiShapePropertyGrid
			// 
			this.uiShapePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiShapePropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.uiShapePropertyGrid.Name = "uiShapePropertyGrid";
			this.uiShapePropertyGrid.Size = new System.Drawing.Size(360, 381);
			this.uiShapePropertyGrid.TabIndex = 0;
			// 
			// ShapeSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(569, 434);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.uiCancelButton);
			this.Controls.Add(this.uiApplyButton);
			this.Name = "ShapeSelectionForm";
			this.Text = "ShapeSelectionForm";
			this.Load += new System.EventHandler(this.ShapeSelectionForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button uiApplyButton;
		private System.Windows.Forms.Button uiCancelButton;
		private System.Windows.Forms.ListView uiShapeList;
		private System.Windows.Forms.SplitContainer splitContainer1;
#pragma warning disable 169
		private System.Windows.Forms.PropertyGrid GridSite;
#pragma warning restore 169
		private System.Windows.Forms.PropertyGrid uiShapePropertyGrid;
	}
}