using System;

namespace Gleed2D.Core
{
	public class TextureCreationProperties : IEntityCreationProperties
	{
		public TextureCreationProperties( string pathToTexture )
		{
			PathToTexture = pathToTexture;
		}

		public string PathToTexture { get; private set; }

		public Type PluginType
		{
			get;
			set ;
		}
	}
}