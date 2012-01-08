using System ;

namespace Gleed2D.InGame
{
	public class PathToFolderChangedEventArgs : EventArgs
	{
		public string ChosenFolder
		{
			get ;
			set ;
		}

		public bool Cancel
		{
			get;
			set ;
		}
	}
}