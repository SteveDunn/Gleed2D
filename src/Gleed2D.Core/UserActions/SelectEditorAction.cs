namespace Gleed2D.Core.UserActions
{
	public class SelectEditorAction : IUserAction
	{
		readonly ItemEditor _item ;

		public SelectEditorAction( ItemEditor item )
		{
			_item = item ;
		}

		public void Process( )
		{
			IoC.Model.SelectEditor( _item ) ;
		}
	}
}