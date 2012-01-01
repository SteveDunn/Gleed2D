using System.IO ;

namespace Gleed2D.Core
{
	public class PathToFile
	{
		public string AbsolutePath
		{
			get ;
			set ;
		}

		public bool Exists
		{
			get
			{
				return File.Exists( AbsolutePath ) ;
			}
		}
	}
}