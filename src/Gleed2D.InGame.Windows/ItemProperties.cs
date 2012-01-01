using System ;
using System.Diagnostics ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	[DebuggerDisplay("Name={Name}, Id={Id}, Visible={Visible}, Position={Position}")]
	public class ItemProperties
	{
		public ItemProperties()
		{
			CustomProperties = new CustomProperties();
		}

		/// <summary>
		/// The name of this item.
		/// </summary>
		public string Name
		{
			get;
			set ;
		}

		public int Id
		{
			get;
			set ;
		}

		/// <summary>
		/// Should this item be visible?
		/// </summary>
		public bool Visible
		{
			get;
			set ;
		}

		/// <summary>
		/// The item's position in world space.
		/// </summary>
		public Vector2 Position
		{
			get;
			set ;
		}

		/// <summary>
		/// A Dictionary containing any user-defined Properties.
		/// </summary>
		public CustomProperties CustomProperties
		{
			get;
			set ;
		}
	}
}