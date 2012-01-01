using System.Drawing ;
using System.Reflection ;

namespace Gleed2D.Core
{
	public interface ILoadImages
	{
		Image LoadFromResource( Assembly assembly, string resourceName ) ;
	}
}