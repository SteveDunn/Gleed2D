using System;
using System.Xml.Linq ;

namespace Gleed2D.InGame
{
	/// <summary>
	/// A utility class to load levels from within the game.
	/// </summary>
	public static class LevelLoader
	{
		public static Level Load(XElement e)
		{
			TypeLookup.Rehydrate( e );
			return new Level( e ) ;
		}
	}
}
