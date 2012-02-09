using System;

namespace Gleed2D.Core
{
	/// <summary>
	/// Interface to be implemented by event subscribers.
	/// </summary>
	/// <remarks>
	/// Event subscribers implement this interface to allow them to receive events with subjects
	/// of type <typeparamref name="T"/>.
	/// </remarks>
	/// <typeparam name="T">
	/// The event subject type.
	/// </typeparam>
	public interface ISubscriber<T>
	{
		/// <summary>
		/// Receives an event of type <typeparamref name="T"/> on the subscriber.
		/// </summary>
		/// <remarks>
		/// The event hub will call this method on the subscriber whenever an appropriate event has been
		/// published to the hub.
		/// </remarks>
		/// <param name="subject">
		/// The event subject.
		/// </param>
		void Receive(T subject);
	}
}