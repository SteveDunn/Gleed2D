using System ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Krypton
{
	public class CircularHullProperties : ItemProperties
	{
		public float Radius
		{
			get;
			set ;
		}

		public int Sides
		{
			get;
			set ;
		}

		public float Opacity
		{
			get;
			set ;
		}

		/// <summary>
		/// The item's scale vector.
		/// </summary>
		public Vector2 Scale
		{
			get ;
			set ;
		}

		public Color FillColor
		{
			get;
			set ;
		}
	}
}