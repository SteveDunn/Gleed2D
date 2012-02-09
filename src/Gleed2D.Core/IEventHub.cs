using System;
using System.Threading;

namespace Gleed2D.Core
{
	/// <summary>
	/// Interface to the event hub service.
	/// </summary>
	/// <remarks>
	/// The event hub provides a many-to-many pub/sub service.
	/// </remarks>
	public interface IEventHub
	{
		/// <summary>
		/// Publishes an event with the given subject to all subscribers.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subject">
		/// The subject.
		/// </param>
		void Publish<T>(T subject);

		/// <summary>
		/// Publishes an event with the given subject to a filtered set of subscribers.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subject">
		/// The subject.
		/// </param>
		/// <param name="publisherFilter">
		/// A predicate that the publisher can use to filter the subscribers that receive the event.
		/// </param>
		void Publish<T>(T subject, Predicate<ISubscriber<T>> publisherFilter);

		/// <summary>
		/// Adds a subscriber for a given subject type.
		/// </summary>
		/// <remarks>
		/// <see cref="SynchronizationContext.Current"/> is used as the <see cref="SynchronizationContext"/>.
		/// </remarks>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subscriber">
		/// The subscriber.
		/// </param>
		void Subscribe<T>(ISubscriber<T> subscriber);

		/// <summary>
		/// Adds a subscriber for a given subject type. The subscriber will be notified via the specified
		/// <see cref="synchronizationContext"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subscriber">
		/// The subscriber.
		/// </param>
		/// <param name="synchronizationContext">
		/// The <see cref="SynchronizationContext"/> via which the subscriber will be notified.
		/// </param>
		void Subscribe<T>(ISubscriber<T> subscriber, SynchronizationContext synchronizationContext);

		/// <summary>
		/// Adds a subscriber for a given subject type. The subscriber is only notified if the specified
		/// predicate matches.
		/// </summary>
		/// <remarks>
		/// <see cref="SynchronizationContext.Current"/> is used as the <see cref="SynchronizationContext"/>.
		/// </remarks>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subscriber">
		/// The subscriber.
		/// </param>
		/// <param name="subscriberFilter">
		/// A predicate that the subscriber can use to filter the events that it receives.
		/// </param>
		void Subscribe<T>(ISubscriber<T> subscriber, Predicate<T> subscriberFilter);

		/// <summary>
		/// Adds a subscriber for a given subject type. The subscriber is only notified if the specified
		/// predicate matches. The subscriber will be notified via the specified
		/// <see cref="SynchronizationContext"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subscriber">
		/// The subscriber.
		/// </param>
		/// <param name="subscriberFilter">
		/// A predicate that the subscriber can use to filter the events that it receives.
		/// </param>
		/// <param name="synchronizationContext">
		/// The <see cref="SynchronizationContext"/> via which the subscriber will be notified.
		/// </param>
		void Subscribe<T>(ISubscriber<T> subscriber, Predicate<T> subscriberFilter, SynchronizationContext synchronizationContext);

		/// <summary>
		/// Unsubscribes an event subscriber.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the subject.
		/// </typeparam>
		/// <param name="subscriber">
		/// The subscriber.
		/// </param>
		void Unsubscribe<T>(ISubscriber<T> subscriber);
	}
}