using Gleed2D.Core.UserActions ;
using JetBrains.Annotations;

namespace Gleed2D.Core
{
	[UsedImplicitly]
	public class HandleUserActions : IHandleUserActions
	{
		public void ProcessAction( IUserAction userAction )
		{
			userAction.Process(  );
		}
	}
}