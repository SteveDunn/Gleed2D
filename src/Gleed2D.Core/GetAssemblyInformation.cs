using System.Reflection ;

namespace Gleed2D.Core
{
	public class GetAssemblyInformation : IGetAssemblyInformation
	{
		public string Version
		{
			get
			{
				return Assembly.GetEntryAssembly( ).GetName( ).Version.ToString( ) ;
			}
		}
	}
}