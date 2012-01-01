using Gleed2D.Core.UserActions ;

namespace Gleed2D.Core
{
	public interface IHandleUserActions
	{
		void ProcessAction( IUserAction userAction ) ;
	}
}