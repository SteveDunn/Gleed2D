namespace Gleed2D.Core.UserActions
{
	public class SelectLayerAction : IUserAction
	{
		readonly Layer _layer ;

		public SelectLayerAction( Layer layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.SelectLayer( _layer ) ;
		}
	}
}