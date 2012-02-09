namespace Gleed2D.Core.UserActions
{
	public class SelectLayerAction : IUserAction
	{
		readonly LayerEditor _layer ;

		public SelectLayerAction( LayerEditor layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.SelectLayer( _layer ) ;
		}
	}
}