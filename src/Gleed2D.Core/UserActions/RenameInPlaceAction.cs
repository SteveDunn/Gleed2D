using Gleed2D.Core.Controls ;

namespace Gleed2D.Core.UserActions
{
	public class RenameInPlaceAction : IUserAction
	{
		public void Process( )
		{
			IMainForm mainForm = IoC.MainForm ;

			LevelExplorerControl uiLevelExplorer = mainForm.LevelExplorer ;
			
			uiLevelExplorer.StartRename( ) ;
		}
	}
}