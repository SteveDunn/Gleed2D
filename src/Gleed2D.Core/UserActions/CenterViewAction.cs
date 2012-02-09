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
			ICanvas canvas = IoC.Canvas ;

			if (_treeItem is LevelEditor)
			{
				canvas.Camera.Position = Microsoft.Xna.Framework.Vector2.Zero;
			}
		
			if (_treeItem is ItemEditor)
			{
				var i = (ItemEditor)_treeItem;
				canvas.Camera.Position = i.ItemProperties.Position;
			}
		}
	}
}