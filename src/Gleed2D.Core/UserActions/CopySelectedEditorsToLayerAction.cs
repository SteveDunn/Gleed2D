namespace Gleed2D.Core.UserActions
{
	public class CopySelectedEditorsToLayerAction : IUserAction
	{
		readonly LayerEditor _layer ;

		public CopySelectedEditorsToLayerAction(LayerEditor layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.CopySelectedItemsToLayer( _layer ) ;
		}
	}
}