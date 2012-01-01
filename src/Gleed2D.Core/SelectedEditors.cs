using System ;
using System.Collections.Generic ;
using System.Linq ;

namespace Gleed2D.Core
{
	/// <summary>
	/// Represents a type that contains editors from the same layer.
	/// </summary>
	public class SelectedEditors 
	{
		readonly List<ItemEditor> _items ;
		
		public SelectedEditors(IEnumerable<ItemEditor> editors  )
		{
			_items=new List<ItemEditor>(editors);

			ensureSameLayer( ) ;
		}

		void ensureSameLayer( )
		{
			var groupBy = _items.GroupBy( ce=>ce.ParentLayer ) ;

			int amountOfDifferentLayers = groupBy.Count() ;
			
			if (amountOfDifferentLayers > 1)
			{
				throw new NotSupportedException(
					@"There should only be 1 common layer for the selected editors, but there were {0}.".FormatWith( amountOfDifferentLayers ) ) ;
			}
		}

		public IEnumerable<ItemEditor> Items
		{
			get
			{
				return _items ;
			}
		}

		public int Count
		{
			get
			{
				return _items.Count ;
			}
		}
	}
}