namespace Gleed2D.Core.UserActions
{
	public class MoveItemUpAction : IUserAction
	{
		readonly ITreeItem _item ;

		public MoveItemUpAction( ITreeItem item )
		{
			_item = item ;
		}

		public void Process( )
		{
			IModel model = IoC.Model ;

			if( _item is LayerEditor )
			{
				var layer = (LayerEditor) _item ;

				if( layer.ParentLevel.Layers.IndexOf( layer ) > 0 )
				{
					model.MoveLayerUp( layer ) ;
				}
			}

			if( _item is ItemEditor )
			{
				var item = (ItemEditor) _item ;

				if( item.ParentLayer.Items.IndexOf( item ) > 0 )
				{
					model.MoveEditorUp( item ) ;
				}
			}
		}
	}
}