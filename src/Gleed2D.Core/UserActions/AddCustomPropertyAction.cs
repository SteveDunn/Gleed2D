using System.Windows.Forms ;
using Gleed2D.Core.Forms ;
using Gleed2D.InGame ;

namespace Gleed2D.Core.UserActions
{
	public class AddCustomPropertyAction : IUserAction
	{
		readonly ITreeItem _treeItem ;

		public AddCustomPropertyAction(ITreeItem treeItem )
		{
			_treeItem = treeItem ;
		}

		public void Process( )
		{
			CustomProperties customProperties = _treeItem.ItemProperties.CustomProperties ;

			var form = new AddCustomPropertyForm( e=> !customProperties.ContainsKey(e) ) ;

			DialogResult result = form.ShowDialog( ) ;

			if( result == DialogResult.OK )
			{
				IoC.Model.AddCustomProperty( _treeItem, form.NewCustomProperty ) ;
			}
		}
	}
}