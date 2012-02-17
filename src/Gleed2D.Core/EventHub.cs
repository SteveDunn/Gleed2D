using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using JetBrains.Annotations;

namespace Gleed2D.Core
{
	[UsedImplicitly]
	public class EventHub : IEventHub
	{
		//Type is the subject type, and the list is all the subscriber information for that subject
		private readonly IDictionary<Type, IList> _subscribers = new Dictionary<Type, IList>();

		public virtual void Publish<T>(T subject)
		{
			Publish(subject, null);
		}

		public void Publish<T>(T subject, Predicate<ISubscriber<T>> publisherFilter)
		{
			Guard.GenericArgumentNotNull(@"subject", subject);

			foreach (SubscriberInfo<T> subscriberInfo in getSubscriberInfos(subject, publisherFilter))
			{
				if (subscriberInfo.SynchronizationContext == null)
				{
					//no SC, so just notify subscriber on this thread
					subscriberInfo.Subscriber.Receive(subject);
				}
				else
				{
					//marshal notification through SC
					SubscriberInfo<T> info = subscriberInfo;
					subscriberInfo.SynchronizationContext.Send( state => info.Subscriber.Receive(subject), null);
				}
			}
		}

		public void Subscribe<T>(ISubscriber<T> subscriber)
		{
			Subscribe(subscriber, null, SynchronizationContext.Current);
		}

		public void Subscribe<T>(ISubscriber<T> subscriber, SynchronizationContext synchronizationContext)
		{
			Subscribe(subscriber, null, synchronizationContext);
		}

		public void Subscribe<T>(ISubscriber<T> subscriber, Predicate<T> subscriberFilter)
		{
			Subscribe(subscriber, subscriberFilter, SynchronizationContext.Current);
		}

		public void Subscribe<T>(ISubscriber<T> subscriber, Predicate<T> subscriberFilter, SynchronizationContext synchronizationContext)
		{
			Guard.GenericArgumentNotNull("subscriber", subscriber);
			List<SubscriberInfo<T>> subscriberInfoList = getSubscriberInfoList<T>();

			if (subscriberInfoList == null)
			{
				subscriberInfoList = new List<SubscriberInfo<T>>();
				_subscribers[typeof(T)] = subscriberInfoList;
			}

			subscriberInfoList.Add(new SubscriberInfo<T>(subscriber, subscriberFilter, synchronizationContext));
		}

		public void Unsubscribe<T>(ISubscriber<T> subscriber)
		{
			Guard.GenericArgumentNotNull("subscriber", subscriber);
			List<SubscriberInfo<T>> subscriberInfoList = getSubscriberInfoList<T>();

			if (subscriberInfoList != null)
			{
				subscriberInfoList.RemoveAll(subscriberInfo => ReferenceEquals(subscriberInfo.Subscriber, subscriber));
			}
		}

		[CanBeNull]
		List<SubscriberInfo<T>> getSubscriberInfoList<T>()
		{
			if (_subscribers.ContainsKey(typeof(T)))
			{
				Debug.Assert(_subscribers[typeof(T)] is List<SubscriberInfo<T>>);
				return _subscribers[typeof(T)] as List<SubscriberInfo<T>>;
			}

			return null;
		}

		IEnumerable<SubscriberInfo<T>> getSubscriberInfos<T>(T subject, Predicate<ISubscriber<T>> publisherFilter)
		{
			List<SubscriberInfo<T>> subscriberInfos = getSubscriberInfoList<T>();

			if (subscriberInfos != null)
			{
				foreach (SubscriberInfo<T> subscriberInfo in subscriberInfos)
				{
					if (publisherFilter == null || publisherFilter(subscriberInfo.Subscriber))
					{
						if (subscriberInfo.Filter == null || subscriberInfo.Filter(subject))
						{
							yield return subscriberInfo;
						}
					}
				}
			}
		}

		private struct SubscriberInfo<T>
		{
			//TODO: use WeakReference to store subscriber? Or perhaps include assertions that there are no
			//subscribers when the service is disposed / torn down
			private readonly ISubscriber<T> _subscriber;
			private readonly Predicate<T> _filter;
			private readonly SynchronizationContext _synchronizationContext;

			public ISubscriber<T> Subscriber
			{
				get
				{
					return _subscriber;
				}
			}

			public Predicate<T> Filter
			{
				get
				{
					return _filter;
				}
			}

			public SynchronizationContext SynchronizationContext
			{
				get
				{
					return _synchronizationContext;
				}
			}

			public SubscriberInfo(ISubscriber<T> subscriber, Predicate<T> filter, SynchronizationContext synchronizationContext)
			{
				_subscriber = subscriber;
				_filter = filter;
				_synchronizationContext = synchronizationContext;
			}
		}

		protected void ClearAllSubscribers()
		{
			_subscribers.Clear();
		}
	}
}