using System;
using System.Collections.Generic;
using System.Drawing ;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq ;
using System.Xml.XPath ;
using StructureMap ;

namespace Gleed2D.Core.Controls
{
	public partial class AssetsControl : UserControl
	{
		Cursor _dragCursor;

		public event EventHandler<EntityChosenEventArgs> AssetChosenByDoubleClicking;

		readonly Dictionary<string, List<IPlugin>> _pluginsForCategories ;

		ImageList _imageList ;

		public AssetsControl()
		{
			InitializeComponent();

			_pluginsForCategories = new Dictionary<string, List<IPlugin>>();
		}

		XElement buildXml()
		{
			var xml = new XElement( @"Items" );
			_pluginsForCategories.ForEach(
				entry =>
					{
						var parent = xml;

						string[ ] strings = entry.Key.Split( '/' ) ;
						foreach( string value in strings )
						{
							var child = parent.XPathSelectElement( @"./Item[@Name='{0}']".FormatWith( value ) ) ;
						
							if (child == null)
							{
								var newChild = new XElement(@"Item", new XAttribute(@"Name",value));
								parent.Add(newChild);
								parent = newChild;
							}
							else
							{
								parent = child ;
							}
						}

						entry.Value.ForEach(
							p => parent.Add( new XElement( @"Item", new XAttribute( @"Hash", p.GetHashCode( ).ToString( ) ) ) ) ) ;
					} ) ;
			
			return xml ;
		}

		void assetsControlLoad(object sender, EventArgs e)
		{
			if( Helper.IsInDesignMode )
			{
				return ;
			}
			
			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;
			var imageRepository=ObjectFactory.GetInstance<IImageRepository>( ) ;
			
			_imageList = imageRepository.CreateImageList( ) ;
			uiList.SmallImageList = _imageList ;

			var plugins = new List<IPlugin>();

			plugins.AddRange( extensibility.EditorPlugins );
			plugins.AddRange( extensibility.BehaviourPlugins );

			plugins.ForEach( p => imageRepository.Set( p.Icon ) );

			plugins.GroupBy( p => p.CategoryName ).Distinct( ).ForEach(
				grouping => _pluginsForCategories.Add( grouping.Key, new List<IPlugin>( ) ) ) ;

			plugins.ForEach( p=> _pluginsForCategories[p.CategoryName].Add( p ) );

			XElement xml = buildXml( ) ;

			populateTreeFromXml( xml ) ;
		}

		void populateTreeFromXml(XElement xml)
		{
			uiTree.Nodes.Clear( ) ; // Clear any existing items
			uiTree.BeginUpdate( ) ; // Begin updating the treeview
			TreeNode treenode = uiTree.Nodes.Add( @"Items" ) ;

			IEnumerable<XElement> baseNodeList = xml.Elements( @"Item" ) ;
			// Get all first level <folder> nodes

			foreach( var xmlnode in baseNodeList )
				// loop through all base <folder> nodes 
			{
				string title = xmlnode.Attribute( @"Name" ).Value ;

				treenode = uiTree.Nodes.Add( title ) ; // add it to the tree

				populateChildNodes( xmlnode, treenode ) ; // Get the children
			}

			uiTree.EndUpdate( ) ; // Stop updating the tree
			uiTree.Refresh( ) ; // refresh the treeview display
		}

		void populateChildNodes(XElement oldXmlnode, TreeNode oldTreenode)
		{
			TreeNode treenode = null ;
		
			var childNodeList = oldXmlnode.Elements() ;
			// Get all children for the past node (parent)

			foreach( var xmlnode in childNodeList )
				// loop through all children
			{
				var hashAttribute = xmlnode.Attribute( @"Hash" ) ;
				if (hashAttribute != null)
				{
					
					var hash = (long) hashAttribute ;
					var tag = oldTreenode.Tag as List<IPlugin> ;
					
					if( tag == null )
					{
						tag= new List<IPlugin>( ) ;
						oldTreenode.Tag = tag ;
					}
					
					IEnumerable<IPlugin> allPlugins = _pluginsForCategories.SelectMany( kvp=>kvp.Value ) ;
					
					IPlugin plugin = allPlugins.Single( p => p.GetHashCode( ) == hash ) ;
					
					tag.Add( plugin );
				}
				else
				{
					string title = xmlnode.CertainAttribute( @"Name" ).Value ;
					treenode = oldTreenode.Nodes.Add( title ) ;
					populateChildNodes( xmlnode, treenode ) ;
				}
			}
		}

		private void uiTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var plugins = e.Node.Tag as List<IPlugin> ;
			if( plugins == null )
			{
				return ;
			}
			
			uiList.BeginUpdate(  );
			
			uiList.Clear(  );
			
			plugins.ForEach( p=>
			                 	{
			                 		var item = new ListViewItem
			                 			{
										   Text = p.Name,
										   ImageKey = p.Icon.Name,
										   Tag=p
			                 			} ;
			                 		
									uiList.Items.Add( item ) ;
			                 	});
			
			uiList.EndUpdate(  );
		}

		void uiList_DragDrop(object sender, DragEventArgs e)
		{
			uiList.Cursor = Cursors.Default ;
			IoC.MainForm.SetCursorForCanvas( Cursors.Default ) ;
		}

		void uiList_DragEnter(object sender, DragEventArgs e)
		{

		}

		void uiList_DragOver(object sender, DragEventArgs e)
		{
		}

		void uiList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			e.UseDefaultCursors = false;
			Cursor.Current = _dragCursor;
			IoC.MainForm.SetCursorForCanvas(_dragCursor);
		}

		void uiList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			var item = (ListViewItem)e.Item;
			
			IoC.MainForm.SetToolStripStatusLabel1( item.ToolTipText );
			
			var bitmap = new Bitmap(uiList.SmallImageList.Images[item.ImageKey]);
			
			_dragCursor = new Cursor(bitmap.GetHicon());

			var plugin = item.Tag as IPlugin ;

			var dragging = new HandleDraggingOfAssets( plugin.CreateDragDropHandler(new EntityCreationProperties(plugin.GetType(), UiAction.Dragging)) ) ;

			uiList.DoDragDrop(dragging, dragging.DragDropEffects);
		}

		void uiList_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem focusedItem = uiList.FocusedItem;

			if (AssetChosenByDoubleClicking != null)
			{
				var creationProperties = new EntityCreationProperties(focusedItem.Tag.GetType(), UiAction.Dragging);

				AssetChosenByDoubleClicking(
					this,
					new EntityChosenEventArgs
						{
							EntityCreationProperties = creationProperties
						} ) ;
			}
		}
	}
}
