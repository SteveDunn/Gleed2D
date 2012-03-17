using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	public class LevelProperties : ItemProperties
	{
	    public string ContentRootFolder 
		{
			get;
			set ;
		}

		public int NextItemNumber
		{
			get;
			set ;
		}

		public Vector2 CameraPosition 
		{
			get;
			set ;
		}

		public string Version 
		{
			get;
			set ;
		}
	}
}