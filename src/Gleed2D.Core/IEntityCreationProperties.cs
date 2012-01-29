using System;

namespace Gleed2D.Core
{
	public interface IEntityCreationProperties
	{
		UiAction TriggeredBy { get; }
		Type PluginType { get; }
	}
}