using System ;
using System.Collections.Generic ;
using System.Reflection ;
using Microsoft.Xna.Framework.Graphics ;
using System.IO ;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;

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
				var stream = new FileStream( fullPathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
                
                Texture2D loadedTexture = Texture2D.FromStream(gd, stream);

                _textures[fullPathToFile] = processTexture(loadedTexture);
				
				stream.Close( );
			}

			return _textures[ fullPathToFile ];
		}

        private Texture2D processTexture(Texture2D texture)
        {
            //Some programs such as Photoshop save PNG and other RGBA images without premultiplied alpha
            if (!Constants.Instance.PremultipliedAlpha)
            {

                //XNA 4.0+ assumes that all RGBA images have premultiplied alpha channels.
                //Code snippet borrowed from http://xboxforums.create.msdn.com/forums/p/62320/383015.aspx#485897

                Byte4[] data = new Byte4[texture.Width * texture.Height];
                texture.GetData<Byte4>(data);
                for (int i = 0; i < data.Length; i++)
                {
                    Vector4 vec = data[i].ToVector4();
                    float alpha = vec.W / 255.0f;
                    int a = (int)(vec.W);
                    int r = (int)(alpha * vec.X);
                    int g = (int)(alpha * vec.Y);
                    int b = (int)(alpha * vec.Z);
                    uint packed = (uint)(
                        (a << 24) +
                        (b << 16) +
                        (g << 8) +
                        r
                        );

                    data[i].PackedValue = packed;
                }

                texture.SetData<Byte4>(data);
            }

            return texture;
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
