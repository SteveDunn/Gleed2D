using System.ComponentModel.Composition ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	[Export( typeof( IPluginGroup ) )]
	public class KryptonPluginGroup : IPluginGroup
	{
		public void Initialise( )
		{
			var lightingState = LightingState.FromDiskOrDefault ;

			IoC.Register( lightingState ) ;
			
			IoC.Register( this ) ;

			var lightingMenu = new ToolStripMenuItem( @"Lighting" ) ;

			var onMenu = new ToolStripMenuItem( @"Lights on" )
				{
					CheckOnClick = true,
					Checked = lightingState.LightingOn
				} ;

			onMenu.CheckedChanged += ( s, e ) => lightingState.LightingOn = onMenu.Checked ;

			ToolStripItemCollection items = lightingMenu.DropDownItems ;
			items.Add( onMenu ) ;

			var menuItems = ObjectFactory.GetInstance<IMenuItems>( ) ;

			menuItems.InsertItemBefore( lightingMenu, menuItems.HelpMenu ) ;
		}

		public string Name
		{
			get
			{
				return @"Krypton plugins" ;
			}
		}
	}
}