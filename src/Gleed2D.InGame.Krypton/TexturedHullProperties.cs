using System ;
using System.IO ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Krypton
{
	public class TexturedHullProperties : ItemProperties
	{
		/// <summary>
		/// The item's rotation in radians.
		/// </summary>
		public float Rotation
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

        /// <summary>
        /// The color to tint the item's texture with (use white for no tint).
        /// </summary>
        public Color TintColor
		{
			get;
			set ;
		}

        /// <summary>
        /// If true, the texture is flipped horizontally when drawn.
        /// </summary>
        public bool FlipHorizontally
		{
			get;
			set ;
		}


        /// <summary>
        /// If true, the texture is flipped vertically when drawn.
        /// </summary>
        public bool FlipVertically
		{
			get;
			set ;
		}

		public bool IsTemplate
		{
			get ;
			set ;
		}


		/// <summary>
		/// The path to the texture's filename (including the extension) relative to ContentRootFolder.
		/// </summary>
		public string TexturePathRelativeToContentRoot
		{
			get;
			set ;
		}

        /// <summary>
        /// The <see cref="TexturePathRelativeToContentRoot"/>without extension. For using in Content.Load<Texture2D/>().
        /// </summary>
        public string AssetName
		{
			get
			{
				return Path.GetFileNameWithoutExtension( TexturePathRelativeToContentRoot ) ;
			}
		}

		/// <summary>
		/// The item's origin relative to the upper left corner of the texture. Usually the middle of the texture.
		/// Used for placing and rotating the texture when drawn.
		/// </summary>
		public Vector2 Origin
		{
			get;
			set ;
		}
	}
}