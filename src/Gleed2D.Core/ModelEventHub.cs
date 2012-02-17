using System;

namespace Gleed2D.Core
{
	public class ModelEventHub : EventHub, IModelEventHub
	{
		public void ClearAllSubscribers()
		{
			base.ClearAllSubscribers();
		}
	}

	public interface IModelEventHub : IEventHub
	{
		void ClearAllSubscribers();
	}
}