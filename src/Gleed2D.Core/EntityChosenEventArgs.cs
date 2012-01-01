using System ;

namespace Gleed2D.Core
{
	public class EntityChosenEventArgs : EventArgs
	{
		public EntityCreationProperties EntityCreationProperties
		{
			get ;
			set ;
		}
	}
}