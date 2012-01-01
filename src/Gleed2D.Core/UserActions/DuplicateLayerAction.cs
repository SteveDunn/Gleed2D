namespace Gleed2D.Core.UserActions
{
	public class DuplicateLayerAction : IUserAction
	{
		readonly Layer _layer ;

		public DuplicateLayerAction( Layer layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.DuplicateLayer( _layer ) ;
		}
	}
}