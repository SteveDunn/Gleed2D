using System.Collections.Generic ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	public class PathItemProperties : ItemProperties
	{
		public bool IsPolygon
		{
			get;
			set ;
		}

		public int LineWidth
		{
			get;
			set ;
		}

		public Color LineColor 
		{
			get;
			set ;
		}

		public List<Vector2> LocalPoints
		{
			get ;
			set ;
		}

		public List<Vector2> WorldPoints
		{
			get ;
			set ;
		}
	}
}