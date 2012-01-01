using System ;

namespace Gleed2D.Core.Controls
{
	public class TexturePickedEventArgs : EventArgs
	{
		public string Path
		{
			get;
			private set ;
		}

		public TexturePickedEventArgs( string path )
		{
			Path = path ;
		}
	}
}