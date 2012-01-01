namespace Gleed2D.Core.UserActions
{
	public class SelectLevelAction : IUserAction
	{
		public SelectLevelAction( )
		{
		}

		public void Process( )
		{
			IoC.Model.SelectLevel( ) ;
		}
	}
}