using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins
{
	public class ShapeDragDropHandler : IHandleDragDrop
	{
		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;
		
		readonly Func<IEditor, ItemEditor> _buildEntityCreationProperties ;

		public ShapeDragDropHandler( Func<IEditor, ItemEditor> buildEntityCreationProperties  )
		{
			_buildEntityCreationProperties = buildEntityCreationProperties ;
		}

		public void WhenDroppedOntoEditor( IEditor editor )
		{
			editor.AddNewItemAtMouse(_buildEntityCreationProperties(editor) ) ;
		}

		public void WhenBeingDraggedOverEditor( IEditor editor, DragEventArgs dragEventArgs )
		{
			dragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( DragEventArgs dragEventArgs )
		{
		}

		public void WhenLeavingEditor( DragEventArgs dragEventArgs )
		{
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return DRAG_DROP_EFFECTS;
			}
		}
	}

	[Export(typeof(IEditorPlugin))]
	public class RectangleEditorPlugin : IEditorPlugin
	{
		readonly ShapeDragDropHandler _dragDropHandler ;

		public RectangleEditorPlugin(  )
		{
			_dragDropHandler = new ShapeDragDropHandler( whenDroppedOntoEditor ) ;
		}

		ItemEditor whenDroppedOntoEditor( IEditor editor )
		{
			return new RectangleItemEditor( ) ;
		}

		public Type EditorType
		{
			get
			{
				return typeof( RectangleItemEditor ) ;
			}
		}

		public Control ControlForAboutBox
		{
			get
			{
				return new PluginDescriptionControl( this ) ;
			}
		}

		public string CategoryName
		{
			get
			{
				return @"Editors/Shapes" ;
			}
		}

		public void InitialiseInUi( IMainForm mainForm )
		{
			ICategoryTabPage tab = mainForm.TryGetTabForCategory( CategoryName ) ;
			
			if( tab==null )
			{
				tab= buildTab( ) ;
				
				mainForm.AddCategoryTab( tab ) ;
			}

			tab.AddPlugin( this ) ;
		}

		ICategoryTabPage buildTab( )
		{
			var tabPage = new DefaultCategoryTabPage
				{
					CategoryName = CategoryName
				} ;
			
			return tabPage ;
		}

		public string Name
		{
			get
			{
				return @"Rectangle" ;
			}
		}

		public ImageProperties ToolboxImage
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_rectangle.png" ) ;
			}
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_rectangle.png" ) ;
			}
		}

		public IHandleDragDrop DragDropHandler
		{
			get
			{
				return _dragDropHandler ;
			}
		}
	}
}