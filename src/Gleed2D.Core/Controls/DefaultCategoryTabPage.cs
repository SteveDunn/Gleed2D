using System;
using System.Drawing ;
using System.Windows.Forms ;

namespace Gleed2D.Core.Controls
{
	public class DefaultCategoryTabPage : TabPage, ICategoryTabPage
	{
		readonly ListView _listView ;
		readonly ImageList _imageList ;
		
		string _categoryName ;

		public DefaultCategoryTabPage( )
		{
			_imageList = new ImageList { ImageSize = new Size(128, 128) };

			_listView = new ListView
				{
					Dock = DockStyle.Fill,
					LargeImageList = _imageList,
					View = View.LargeIcon
				} ;

			_listView.MouseDoubleClick += listViewMouseDoubleClick;
			_listView.DragDrop += listViewDragDrop;
			_listView.DragEnter += listViewDragEnter;
			_listView.DragLeave += listViewDragLeave;
			_listView.DragOver += listViewDragOver;
			_listView.ItemDrag += listViewItemDrag;

			Gdi.SetListViewSpacing(_listView, 128 + 8, 128 + 32);

			Controls.Add( _listView );
		}

		void listViewItemDrag(object sender, ItemDragEventArgs e)
		{
			var item = (ListViewItem)e.Item;
			
			IoC.MainForm.SetToolStripStatusLabel1( item.ToolTipText );
			
			var bitmap = new Bitmap(_listView.LargeImageList.Images[item.ImageKey]);
			
			new Cursor(bitmap.GetHicon());

			var type = item.Tag as Type ;

			var plugin = (IPlugin)Activator.CreateInstance(type);
			var creationProperties = new EntityCreationProperties(type, UiAction.Dragging);

			var handlerForPlugin = plugin.CreateDragDropHandler(creationProperties);

			_listView.DoDragDrop(new HandleDraggingOfAssets(handlerForPlugin), DragDropEffects.Move);
		}

		private void listViewDragOver(object sender, DragEventArgs e)
		{
			
		}

		private void listViewDragLeave(object sender, EventArgs e)
		{
			
		}

		private void listViewDragEnter(object sender, DragEventArgs e)
		{
			
		}

		private void listViewDragDrop(object sender, DragEventArgs e)
		{
			
		}

		void listViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			var type = _listView.FocusedItem.Tag as Type;

			var creationProperties = new EntityCreationProperties(type, UiAction.DoubleClicking);

			IoC.Canvas.StartCreatingEntityAfterNextClick( creationProperties ) ;
		}

		public string CategoryName
		{
			get
			{
				return _categoryName ;
			}
			set
			{
				_categoryName = value ;
				Name = value ;
				Text = value ;
			}
		}

		public void AddPlugin(IEditorPlugin editorPlugin)
		{
			_imageList.Images.Add( editorPlugin.Name, editorPlugin.ToolboxImage.Picture ) ;

			ListViewItem listViewItem = _listView.Items.Add( editorPlugin.Name, editorPlugin.Name ) ;

			listViewItem.Tag = editorPlugin.GetType();
		}
	}
}