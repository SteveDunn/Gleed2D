using System.Runtime.InteropServices ;
using System.Windows.Forms ;

namespace Gleed2D.Core
{
	public static class Gdi
	{
		public static void SetListViewSpacing( ListView lst, int x, int y )
		{
			SendMessage( (int) lst.Handle, 0x1000 + 53, 0, y * 65536 + x ) ;
		}

		[DllImport( "User32.dll" )]
		static extern int SendMessage( int handle, int wMsg, int wParam, int lParam ) ;
	}
}