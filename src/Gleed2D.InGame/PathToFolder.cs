using System ;
using System.IO ;

namespace Gleed2D.InGame
{
	public class PathToFolder
	{
		public event EventHandler<PathToFolderChangedEventArgs> PathChanging ;

		string _absolutePath ;

		public PathToFolder( string path )
		{
			AbsolutePath = path ;
		}

		public PathToFolder( )
		{
		}

		public string AbsolutePath
		{
			get
			{
				return _absolutePath ;
			}
			set
			{
				if( _absolutePath == value )
				{
					return ;
				}
				
				bool shouldCancel = false ;
				
				var handler = PathChanging ;
				
				if( handler != null )
				{
					var args = new PathToFolderChangedEventArgs
						{
							Cancel = false,
							ChosenFolder = value
						} ;
					
					handler(this, args);
					shouldCancel = args.Cancel ;
				}

				if (shouldCancel)
				{
					return ;
				}
				
				_absolutePath = value ;
			}
		}

		public bool Exists
		{
			get
			{
				return Directory.Exists( AbsolutePath ) ;
			}
		}

	}
}