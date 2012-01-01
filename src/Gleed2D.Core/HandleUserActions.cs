using Gleed2D.Core.UserActions ;

namespace Gleed2D.Core
{
	public class HandleUserActions : IHandleUserActions
	{
		public void ProcessAction( IUserAction userAction )
		{
			userAction.Process(  );
		}
	}
}