using System ;
using System.Collections.Generic ;
using System.Reflection ;
using Microsoft.Xna.Framework.Graphics ;
using System.IO ;

namespace Gleed2D.Core
{
	public class TextureStore : ITextureStore
	{
		readonly IGame _game ;
		readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

		Texture2D _dummyTexture ;

		public TextureStore( IGame game )
		{
			_game = game ;
		}

		public Texture2D FromFile(GraphicsDevice gd, string fullPathToFile)
		{
			if( !_textures.ContainsKey( fullPathToFile ) )
			{
				var stream = new FileStream( fullPathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) ;
		
				_textures[ fullPathToFile ] = Texture2D.FromStream( gd, stream ) ;
				
				stream.Close( ) ;
			}

			return _textures[ fullPathToFile ] ;
		}

		public void Clear()
		{
			_textures.Clear();
		}

		public Texture2D DummyTexture
		{
			get
			{
				if( _dummyTexture == null )
				{
					Assembly executingAssembly = Assembly.GetEntryAssembly( ) ;

					var resStream = executingAssembly.GetManifestResourceStream( "GLEED2D.Resources.circle.png" ) ;

					_dummyTexture = Texture2D.FromStream( _game.GraphicsDevice, resStream ) ;


				}
				return _dummyTexture ;
			}
		}
	}
}
