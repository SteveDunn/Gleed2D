namespace Gleed2D.Core.UserActions
{
	public class MoveSelectedEditorsToLayerAction : IUserAction
	{
		readonly LayerEditor _layer ;

		public MoveSelectedEditorsToLayerAction(LayerEditor layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.MoveSelectedItemsToLayer( _layer );
		}
	}
}