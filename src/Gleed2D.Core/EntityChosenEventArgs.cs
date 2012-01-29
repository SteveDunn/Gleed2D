using System ;

namespace Gleed2D.Core
{
	public class EntityChosenEventArgs : EventArgs
	{
		public IEntityCreationProperties EntityCreationProperties
		{
			get ;
			set ;
		}
	}
}