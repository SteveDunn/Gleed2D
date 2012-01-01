using System.Diagnostics ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	[DebuggerDisplay("Name={Name}, Id={Id}, Visible={Visible}, Position={Position}, Width={Width}, Height={Height}, FillColor={FillColor}, Rotation={Rotation}")]
	public class RectangleItemProperties : ItemProperties, IRotatable
	{
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

		public Color FillColor
		{
			get;
			set ;
		}

		/// <summary>
		/// The item's rotation in radians.
		/// </summary>
		public float Rotation
		{
			get;
			set ;
		}
	}
}