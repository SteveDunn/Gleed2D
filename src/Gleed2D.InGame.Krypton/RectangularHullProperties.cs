using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Krypton
{
	public class RectangularHullProperties : ItemProperties
	{
		/// <summary>
		/// The item's rotation in radians.
		/// </summary>
    	public float Rotation
		{
			get;
			set ;
		}

		public float Width
		{
			get;
			set ;
		}

    	public float Height
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
			get;
			set ;
		}
	}
}