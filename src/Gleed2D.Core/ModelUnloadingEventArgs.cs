using System ;

namespace Gleed2D.Core
{
	public class ModelUnloadingEventArgs : EventArgs
	{
		public ModelUnloadingEventArgs( Level level )
		{
			Level = level ;
		}

		public Level Level
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