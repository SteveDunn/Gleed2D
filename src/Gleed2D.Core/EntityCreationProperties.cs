using System;

namespace Gleed2D.Core
{
	public class EntityCreationProperties : IEntityCreationProperties
	{
		public EntityCreationProperties(Type pluginType)
		{
			PluginType = pluginType;
		}

		public Type PluginType
		{
			get; set;
		}
	}
}