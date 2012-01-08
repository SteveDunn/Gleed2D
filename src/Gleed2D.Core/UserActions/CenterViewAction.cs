namespace Gleed2D.Core.UserActions
{
	public class CenterViewAction : IUserAction
	{
		readonly ITreeItem _treeItem ;

		public CenterViewAction( ITreeItem treeItem )
		{
			_treeItem = treeItem ;
		}

		public void Process( )
		{
			IEditor editor = IoC.Editor ;

			if (_treeItem is Level)
			{
				editor.Camera.Position = Microsoft.Xna.Framework.Vector2.Zero;
			}
		
			if (_treeItem is ItemEditor)
			{
				var i = (ItemEditor)_treeItem;
				editor.Camera.Position = i.ItemProperties.Position;
			}
		}
	}
}