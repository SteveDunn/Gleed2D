namespace Gleed2D.Core.UserActions
{
	public class MoveSelectedEditorsToLayerAction : IUserAction
	{
		readonly Layer _layer ;

		public MoveSelectedEditorsToLayerAction(Layer layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.MoveSelectedItemsToLayer( _layer );
		}
	}
}