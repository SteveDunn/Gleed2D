namespace Gleed2D.Core.UserActions
{
	public class DeleteSelectedEditorsAction : IUserAction
	{
		public void Process( )
		{
			IoC.Model.DeleteSelectedItems( );
		}
	}
}