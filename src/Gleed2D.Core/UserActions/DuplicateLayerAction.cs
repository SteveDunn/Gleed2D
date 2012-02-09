namespace Gleed2D.Core.UserActions
{
	public class DuplicateLayerAction : IUserAction
	{
		readonly LayerEditor _layer ;

		public DuplicateLayerAction( LayerEditor layer )
		{
			_layer = layer ;
		}

		public void Process( )
		{
			IoC.Model.DuplicateLayer( _layer ) ;
		}
	}
}