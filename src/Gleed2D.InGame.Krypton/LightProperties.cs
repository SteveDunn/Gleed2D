using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Krypton
{
	public enum TypeOfShadow
	{
		Solid = 1,
		Illuminated = 2,
		Occluded = 3,
	}

	public enum TypeOfLight
	{
		Conic,
		Point
	}

	public class LightProperties : ItemProperties
	{
		/// <summary>
		/// The item's rotation in radians.
		/// </summary>
		public float Rotation
		{
			get ;
			set ;
		}

		/// <summary>
		/// The type of light
		/// </summary>
		public TypeOfLight TypeOfLight
		{
			get ;
			set ;
		}

		/// <summary>
		/// The item's rotation in radians.
		/// </summary>
		public int TextureSize
		{
			get ;
			set ;
		}

		/// <summary>
		/// The item's field of view in radians.
		/// </summary>
		public float FieldOfView
		{
			get ;
			set ;
		}

		/// <summary>
		/// The item's range.
		/// </summary>
		public float Range
		{
			get ;
			set ;
		}

		/// <summary>
		/// The item's intensity.
		/// </summary>
		public float Intensity
		{
			get ;
			set ;
		}

		/// <summary>
		/// The item's shadow type.
		/// </summary>
		public TypeOfShadow ShadowType
		{
			get ;
			set ;
		}

		/// <summary>
		/// The color of the light.
		/// </summary>
		public Color Color
		{
			get ;
			set ;
		}

		/// <summary>
		/// If true, the light is on, otherwise... it's off.
		/// </summary>
		public bool IsOn
		{
			get ;
			set ;
		}

	}
}