using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IEditorPlugin))]
	public class CircleEditorPlugin : IEditorPlugin
	{
		public Type EditorType
		{
			get
			{
				return typeof( CircleItemEditor ) ;
			}
		}

		public string CategoryName
		{
			get
			{
				return @"Editors/Shapes" ;
			}
		}

		public Control ControlForAboutBox
		{
			get
			{
				return new PluginDescriptionControl( this ) ;
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
					CategoryName = CategoryName,
				} ;
			
			return tabPage ;
		}

		public string Name
		{
			get
			{
				return @"Circle" ;
			}
		}


		public ImageProperties ToolboxImage
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_circle.png" ) ;
			}
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.primitive_circle.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			return new ShapeDragDropHandler(entityCreationProperties);
		}
	}
}