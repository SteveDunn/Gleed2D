using System ;
using System.Collections.Generic ;

namespace Gleed2D.Core
{
	public class ModelChangedEventArgs : EventArgs
	{
		public ModelChangedEventArgs( ITreeItem item )
		{
			ItemsAffected = new[ ]
				{
					item
				} ;
		}

		public ModelChangedEventArgs( IEnumerable<ITreeItem> items )
		{
			ItemsAffected = items ;
		}

		public IEnumerable<ITreeItem> ItemsAffected
		{
			get;
			set ;
		}
	}
}