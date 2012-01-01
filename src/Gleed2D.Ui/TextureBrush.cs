using System;
using Gleed2D.Core ;
using Microsoft.Xna.Framework.Graphics;
using StructureMap ;

namespace GLEED2D
{
    class TextureBrush
    {
		public Texture2D Texture
		{
			get;
			private set ;
		}

        public TextureBrush(string pathToImageFile)
        {
        	var textureStore = ObjectFactory.GetInstance<ITextureStore>( ) ;

        	var game = ObjectFactory.GetInstance<IGame>() ;

			Texture = textureStore.FromFile(game.GraphicsDevice, pathToImageFile);
        }
    }
}
