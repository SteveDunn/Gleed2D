using System ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using JetBrains.Annotations ;

namespace GLEED2D.Forms
{
	public class MainFormMenuItems : IMenuItems
	{
		readonly MenuStrip _menuStrip ;

		public MainFormMenuItems( IMainForm mainForm )
		{
			_menuStrip = mainForm.MenuStrip ;
		}

		public ToolStripMenuItem HelpMenu
		{

			get
			{
				return (ToolStripMenuItem) _menuStrip.Items[ @"helpToolStripMenuItem" ] ;
			}
		}

		[CanBeNull]
		public ToolStripItem TryGetByName( string name )
		{
			if (_menuStrip.Items.ContainsKey(name))
			{
				return _menuStrip.Items[ name ] ;
			}
			
			return null ;
		}

		public void InsertItemBefore( ToolStripMenuItem menuItem, ToolStripItem thingOnRight )
		{
			int indexOfKey = _menuStrip.Items.IndexOf( thingOnRight )  ;
			
			_menuStrip.Items.Insert(Math.Max(indexOfKey,0), menuItem ) ;
		}
	}
}