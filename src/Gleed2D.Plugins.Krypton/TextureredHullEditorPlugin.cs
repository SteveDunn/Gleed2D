using System ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins.Krypton
{
	//todo: consider if this is something that's really needed
	//[Export(typeof(IPrimitivePlugin))]
	public class TextureredHullEditorPlugin : IEditorPlugin
	{
		TextureTabPage _tab ;

		public Type EditorType
		{
			get
			{
				return typeof( TexturedHullEditor ) ;
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
				return @"Lighting/Krypton" ;
			}
		}

		public void InitialiseInUi( IMainForm mainForm )
		{
			_tab = mainForm.TryGetTabForCategory( CategoryName ) as TextureTabPage ;
			
			if( _tab==null )
			{
				_tab= buildTab( ) ;
				
				mainForm.AddCategoryTab( _tab ) ;
			}

			_tab.AddPlugin( this ) ;
		}

		TextureTabPage buildTab( )
		{
			var tabPage = new TextureTabPage
				{
					CategoryName = CategoryName
				} ;

			IoC.Model.NewModelLoaded += ( o, args ) => _tab.SetRootFolder( IoC.Model.Level.ContentRootFolder ) ;

			return tabPage ;
		}

		public string Name
		{
			get
			{
				return @"Textured Hull" ;
			}
		}

		public ImageProperties ToolboxImage
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Krypton.Resources.textured_hull.png" ) ;
			}
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Krypton.Resources.textured_hull.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			throw new NotImplementedException();
		}
	}
}