using System ;
using System.ComponentModel ;
using System.Globalization ;
using System.Windows.Forms ;
using Gleed2D.InGame ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	public class PathToFolderConverter : StringConverter
	{
		public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
		{
			var pathToFolder = (PathToFolder) ( (GridItem) context ).Value  ;
			pathToFolder.AbsolutePath = (string)value ;
			return pathToFolder ;
			//return new PathToFolder
			//    {
			//        AbsolutePath = Convert.ToString( value )
			//    } ;
		}

		public override object ConvertTo(
			ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
		{
			return ( (PathToFolder) value ).AbsolutePath ;
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