using System.Collections.Generic ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Krypton
{
	public class ConvexHullProperties : ItemProperties
	{
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

		public float Opacity
		{
			get;
			set ;
		}
	}
}