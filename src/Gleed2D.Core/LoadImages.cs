using System ;
using System.Drawing ;
using System.IO ;
using System.Reflection ;
using System.Resources ;

namespace Gleed2D.Core
{
	public class LoadImages : ILoadImages
	{
		public Image LoadFromResource( Assembly assembly, string resourceName )
		{
			using( Stream stream = assembly.GetManifestResourceStream( resourceName ) )
			{
				if( stream == null )
				{
					throw new MissingManifestResourceException(
						@"Cannot load {0} from from assembly {1} as it does not exist.".FormatWith( resourceName, assembly ) ) ;
				}

				return Image.FromStream( stream ) ;
			}
		}
	}
}