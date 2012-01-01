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
			_imageList= new ImageList();
			_imageList.ImageSize=new Size(128,128);
			
			_listView = new ListView
				{
					Dock = DockStyle.Fill,
					LargeImageList = _imageList,
					View = View.LargeIcon
				} ;

			_listView.MouseDoubleClick += _listView_MouseDoubleClick;

			Gdi.SetListViewSpacing(_listView, 128 + 8, 128 + 32);

			Controls.Add( _listView );
		}

		void _listView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var creationProperties = (EntityCreationProperties) _listView.FocusedItem.Tag ;

			IoC.Editor.StartCreatingEntityAfterNextClick( creationProperties ) ;
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

			listViewItem.Tag = new EntityCreationProperties
				{
					Name = @"Primitive",
					PluginType = editorPlugin.GetType( )
				} ;
		}
	}
}