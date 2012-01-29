using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IEditorPlugin))]
	public class TextureEditorPlugin : IEditorPlugin
	{
		TextureTabPage _tab ;

		public Type EditorType
		{
			get
			{
				return typeof( TextureItemEditor ) ;
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
				return @"Editors/Textures" ;
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
				return @"Texture" ;
			}
		}

		public ImageProperties ToolboxImage
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.toolbox_texture.png" ) ;
			}
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.toolbox_texture.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			return new TextureDragDropHandler( entityCreationProperties as TextureCreationProperties);
		}
	}
}