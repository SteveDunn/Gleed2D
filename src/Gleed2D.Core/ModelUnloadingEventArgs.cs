using System ;

namespace Gleed2D.Core
{
	public class ModelUnloadingEventArgs : EventArgs
	{
		public ModelUnloadingEventArgs( LevelEditor level )
		{
			Level = level ;
		}

		public LevelEditor Level
		{
			get;
			private set ;
		}

		public bool Cancelled
		{
			get;
			set ;
		}
	}
}