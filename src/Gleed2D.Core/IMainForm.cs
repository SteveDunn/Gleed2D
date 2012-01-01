using System;
using System.Drawing ;
using System.Windows.Forms ;
using Gleed2D.Core.Controls ;

namespace Gleed2D.Core
{
	public interface IMainForm
	{
		event EventHandler<EventArgs> OnShuttingDown ;
		
		LevelExplorerControl LevelExplorer
		{
			get ;
		}

		Size CanvasSize
		{
			get ;
		}

		bool CanvasHasFocus
		{
			get ;
		}

		MenuStrip MenuStrip
		{
			get ;
		}

		void SetCursorForCanvas( Cursor cursor ) ;

		void SetToolStripStatusLabel1( string text ) ;
	
		void SetToolStripStatusLabel2( string text ) ;
		void SetFpsDiagText( string text ) ;
		
		void SetZoomComboText( string text ) ;
		
		void SetToolStripStatusLabel3( string s ) ;

		IntPtr GetHandle( ) ;
		
		void Show( ) ;
		ICategoryTabPage TryGetTabForCategory( string categoryName ) ;
		void AddCategoryTab( ICategoryTabPage categoryTabPage ) ;
	}
}
