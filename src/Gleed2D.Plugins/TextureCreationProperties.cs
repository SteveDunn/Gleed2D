using System;
using Gleed2D.Core;

namespace Gleed2D.Plugins
{
	public class TextureCreationProperties : IEntityCreationProperties
	{
		public TextureCreationProperties( string pathToTexture, UiAction triggeredBy )
		{
			PathToTexture = pathToTexture;
			TriggeredBy = triggeredBy;
		}

		public string PathToTexture { get; private set; }

		public UiAction TriggeredBy
		{
			get;
			private set;
		}

		public Type PluginType
		{
			get { return typeof (TextureEditorPlugin); }
		}
	}
}