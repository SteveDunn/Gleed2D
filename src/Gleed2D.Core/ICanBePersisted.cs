using System.Xml.Linq ;

namespace Gleed2D.Core
{
	public interface ICanBePersisted
	{
		XElement ToXml( ) ;
	}
}