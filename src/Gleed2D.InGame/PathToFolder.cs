using System ;
using System.Diagnostics;
using System.IO ;

namespace Gleed2D.InGame
{
	[DebuggerDisplay("AbsolutePath={AbsolutePath}, Exists={Exists}")]
	public class PathToFolder
	{
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

				if (!value.EndsWith(@"\"))
				{
					value = value + @"\";
				}
				
				//bool shouldCancel = false ;
				
				//var handler = PathChanging ;
				
				//if( handler != null )
				//{
				//    var args = new PathToFolderChangedEventArgs
				//        {
				//            Cancel = false,
				//            ChosenFolder = value
				//        } ;
					
				//    handler(this, args);
				//    shouldCancel = args.Cancel ;
				//}

				//if (shouldCancel)
				//{
				//    return ;
				//}
				
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