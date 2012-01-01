using System ;

namespace Gleed2D.Core
{
	public interface IPluginGroup
	{
		void Initialise( ) ;
		
		string Name
		{
			get ;
		}
	}
}