using System.Windows.Forms ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.Forms ;
using Gleed2D.InGame ;

namespace Gleed2D.Core.UserActions
{
	public class AttachBehaviourAction : IUserAction
	{
		readonly IBehaviour _behaviour ;
		readonly ITreeItem _treeItem ;

		public AttachBehaviourAction(IBehaviour behaviour, ITreeItem target )
		{
			_behaviour = behaviour ;
			_treeItem = target ;
		}

		public void Process( )
		{
			_treeItem.AddBehaviour( _behaviour );
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