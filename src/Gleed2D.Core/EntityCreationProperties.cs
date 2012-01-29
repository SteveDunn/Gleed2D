using System;

namespace Gleed2D.Core
{
	public class EntityCreationProperties : IEntityCreationProperties
	{
		public EntityCreationProperties(Type pluginType, UiAction triggeredBy)
		{
			PluginType = pluginType;
			TriggeredBy = triggeredBy;
		}

		public UiAction TriggeredBy { get; private set; }

		public Type PluginType
		{
			get; private set;
		}
	}
}