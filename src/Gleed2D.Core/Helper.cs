using System ;
using System.ComponentModel ;
using System.Diagnostics;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public static class Helper
	{

		public static bool IsInDesignMode
		{
			get
			{
				bool returnFlag = false ;

#if DEBUG
				if( LicenseManager.UsageMode == LicenseUsageMode.Designtime )
				{
					returnFlag = true ;
				}
				else if( StringComparer.OrdinalIgnoreCase.Compare( Process.GetCurrentProcess( ).ProcessName, @"DEVENV" ) == 0 )
				{
					returnFlag = true ;
				}
#endif

				return returnFlag ;
			}
		}

		public static EdgePosition WhichEdge( Rectangle rect, float angleInRadians, Vector2 point, int edgeWidth )
		{
			var topGrap = new Rectangle( rect.X, rect.Y, rect.Width, edgeWidth ) ;
			var bottomGrab = new Rectangle( rect.X, rect.Bottom - edgeWidth, rect.Width, edgeWidth ) ;
			var leftGrab = new Rectangle( rect.X, rect.Y, edgeWidth, rect.Height ) ;
			var rightGrab = new Rectangle( rect.X + rect.Width - edgeWidth, rect.Y, edgeWidth, rect.Height ) ;

			var center = rect.Center ;


			Vector2 pivot = center.ToVector2( ) ;

			if( topGrap.ToPolygon( ).RotateAroundPoint( pivot, angleInRadians ).ContainsPoint(point ) )
			{
				return EdgePosition.Top ;
			}

			if( bottomGrab.ToPolygon( ).RotateAroundPoint( pivot, angleInRadians ).ContainsPoint(point ) )
			{
				return EdgePosition.Bottom ;
			}

			if( leftGrab.ToPolygon( ).RotateAroundPoint( pivot, angleInRadians ).ContainsPoint(point ) )
			{
				return EdgePosition.Left ;
			}

			if( rightGrab.ToPolygon( ).RotateAroundPoint( pivot, angleInRadians ).ContainsPoint(point ) )
			{
				return EdgePosition.Right ;
			}

			return EdgePosition.None ;
		}
	}
}