using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IEditorPlugin))]
	public class RectangleEditorPlugin : IEditorPlugin
	{
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

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			var handler = new ShapeDragDropHandler(entityCreationProperties);

			//move the 'EntityCreationProperties' into here and pass this method some context, e.g. the editor
			//so it can get the zoom level/store the last handler settings etc.
			return handler;
		}
	}
}