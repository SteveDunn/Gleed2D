using System ;
using System.Collections ;
using System.ComponentModel ;
using System.Drawing.Design ;

namespace Gleed2D.Core.Controls
{
	public sealed class GlyphService : IPropertyValueUIService
	{
		public event EventHandler PropertyUIValueItemsChanged ;
		public event PropertyValueUIHandler QueryPropertyUIValueItems ;

		public PropertyValueUIItem[ ] GetPropertyUIValueItems( ITypeDescriptorContext context, PropertyDescriptor propDesc )
		{
			ArrayList list = null ;
		
			if( QueryPropertyUIValueItems != null )
			{
				list = new ArrayList( ) ;
				QueryPropertyUIValueItems( context, propDesc, list ) ;
			}

			if( list == null || list.Count == 0 )
			{
				return new PropertyValueUIItem[ 0 ] ;
			}
	
			var result = new PropertyValueUIItem[ list.Count ] ;
		
			list.CopyTo( result ) ;
		
			return result ;
		}

		public void NotifyPropertyValueUIItemsChanged( )
		{
			if( PropertyUIValueItemsChanged != null )
			{
				PropertyUIValueItemsChanged( this, EventArgs.Empty ) ;
			}
		}

		void IPropertyValueUIService.RemovePropertyValueUIHandler( PropertyValueUIHandler newHandler )
		{
			QueryPropertyUIValueItems -= newHandler ;
		}

		void IPropertyValueUIService.AddPropertyValueUIHandler( PropertyValueUIHandler newHandler )
		{
			QueryPropertyUIValueItems += newHandler ;
		}

	}
}