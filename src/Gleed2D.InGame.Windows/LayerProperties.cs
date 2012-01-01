using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	public class LayerProperties : ItemProperties
	{
		/// <summary>
		/// The Scroll Speed relative to the main camera. The X and Y components are 
		/// interpreted as factors, so (1;1) means the same scrolling speed as the main camera.
		/// Enables parallax scrolling.
		/// </summary>
		public Vector2 ScrollSpeed
		{
			get;
			set ;
		}
	}
}