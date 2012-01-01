using System ;

namespace Gleed2D.Core
{
	public class CommandEndedArgs : EventArgs
	{
		public Command Command
		{
			get;
			set ;
		}

		public int UndoCount
		{
			get ;
			set ;
		}

		public int RedoCount
		{
			get ;
			set ;
		}
	}
}