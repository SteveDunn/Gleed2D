using System.Windows.Forms ;

namespace Gleed2D.Core
{
	public interface IMenuItems
	{
		ToolStripMenuItem HelpMenu
		{
			get ;
		}

		ToolStripItem TryGetByName( string name ) ;

		void InsertItemBefore( ToolStripMenuItem menuItem, ToolStripItem thingOnRight ) ;
	}
}