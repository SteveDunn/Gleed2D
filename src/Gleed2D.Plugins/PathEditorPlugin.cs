using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IEditorPlugin))]
	public class PathEditorPlugin : IEditorPlugin
	{
		void whenEnteringEditor( DraggingContext draggingContext )
		{
			draggingContext.DragEventArgs.Effect=DragDropEffects.Move;
		}

		void whenDraggingOverEditor( ICanvas canvas, DraggingContext draggingContext )
		{
			draggingContext.DragEventArgs.Effect=DragDropEffects.Move;
		}

		void whenDroppedOntoEditor( ICanvas canvas, DraggingContext draggingContext )
		{
			canvas.StartCreatingEntityNow(
				new EntityCreationProperties(GetType(), UiAction.Dragging));
		}

		public Type EditorType
		{
			get
			{
				return typeof( PathItemEditor ) ;
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
				return @"Path" ;
			}
		}

		public ImageProperties ToolboxImage
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_path.png" ) ;
			}
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_path.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			return new LambdaDrivenDragDropHandler( 
				DragDropEffects.Move,
				whenEnteringEditor: whenEnteringEditor,
				whenDraggingOverEditor: whenDraggingOverEditor,
				whenDroppedOntoEditor: whenDroppedOntoEditor );
		}
	}
}