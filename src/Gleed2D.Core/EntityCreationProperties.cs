using System ;
using System.Collections.Generic ;

namespace Gleed2D.Core
{
	public class EntityCreationProperties
	{
		public EntityCreationProperties( )
		{
			Parameters = new Dictionary<string, string>( ) ;
		}

		public string Name
		{
			get;
			set ;
		}

		public void AddParameter(string name, string value)
		{
			Parameters.Add( name, value );
		}

		public IDictionary<string, string> Parameters
		{
			get;
			private set ;
		}

		public Type PluginType
		{
			get;
			set ;
		}
	}
}