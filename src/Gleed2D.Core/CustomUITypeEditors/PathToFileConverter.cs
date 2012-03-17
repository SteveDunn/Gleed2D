using System ;
using System.ComponentModel ;
using System.Globalization ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	public class PathToFileConverter : StringConverter
	{
		public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
		{
			return new PathToFile
				{
					AbsolutePath = Convert.ToString( value )
				} ;
		}

		public override object ConvertTo(
			ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
		{
			return ( (PathToFile) value ).AbsolutePath ;
		}

		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
		{
			return true ;
		}

		public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType )
		{
			return true ;
		}
	}
}