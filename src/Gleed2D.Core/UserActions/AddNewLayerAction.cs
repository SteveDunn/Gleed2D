using System.Windows.Forms ;
using Gleed2D.Core.Forms ;

namespace Gleed2D.Core.UserActions
{
	public class AddNewLayerAction : IUserAction
	{
		public void Process( )
		{
			var dialog = new AddLayerDialog( checkProposedName ) ;

			DialogResult dialogResult = dialog.ShowDialog() ;

			if( dialogResult == DialogResult.OK )
			{
				IModel model = IoC.Model ;

				model.AddNewLayer( new LayerEditor(model.Level,dialog.ChosenName ) );
			}
		}

		bool checkProposedName( string obj )
		{
			bool alreadyExists = IoC.Model.Level.ContainsAnythingNamed( obj ) ;
			
			return !alreadyExists ;
		}
	}
}