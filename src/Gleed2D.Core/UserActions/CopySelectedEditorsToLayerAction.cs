namespace Gleed2D.Core.UserActions
{
	public class CopySelectedEditorsToLayerAction : IUserAction
	{
		readonly Layer _layer ;

		public CopySelectedEditorsToLayerAction(Layer layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.CopySelectedItemsToLayer( _layer ) ;
		}
	}
}