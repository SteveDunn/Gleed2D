namespace Gleed2D.Core.UserActions
{
	public class AddEditorToSelectionAction : IUserAction
	{
		readonly ItemEditor _editor ;

		public AddEditorToSelectionAction( ItemEditor editor )
		{
			_editor = editor ;
		}

		public void Process( )
		{
			IoC.Model.AddEditorToSelection( _editor ) ;
		}
	}
}