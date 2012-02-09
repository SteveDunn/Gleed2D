namespace Gleed2D.Core.UserActions
{
	public class MoveItemDownAction : IUserAction
	{
		readonly ITreeItem _treeItem ;

		public MoveItemDownAction( ITreeItem treeItem )
		{
			_treeItem = treeItem ;
		}

		public void Process( )
		{
			IModel model = IoC.Model ;

			if (_treeItem is LayerEditor)
			{
				var layer = (LayerEditor)_treeItem;
				
				if (layer.ParentLevel.Layers.IndexOf(layer) < layer.ParentLevel.Layers.Count - 1)
				{
					model.MoveLayerDown(layer);
				}
			}
			
			if (_treeItem is ItemEditor)
			{
				var item = (ItemEditor)_treeItem;
				if (item.ParentLayer.Items.IndexOf(item) < item.ParentLayer.Items.Count - 1)
				{
					model.MoveEditorDown(item);
				}
			}
		}
	}
}