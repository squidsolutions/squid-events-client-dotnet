using System;

namespace SquidSolutions
{
	public class EventTracker
	{

		static readonly object padlock = new object();

		public static EventTrackerClient Client { get; private set; }

		/// <summary>
		/// Initialize the EventTracker with the Config.
		/// </summary>
		/// <param name="config">Config.</param>
		public static void Initialize(Config config)
		{
			lock (padlock)
			{
				if (Client == null || !Client.IsRunning()) {
					Client = new EventTrackerClient (config);
				}
			}
		}

		public static void Send(EventModel eventModel) {
			if (Client != null) {
				Client.Send (eventModel);
			}
		}

		/// <summary>
		///  Shutdown the EventTracker. 
		///  Once that method is called, the service will stop accepting new events.
		///  The method will then flush the queue. Depending of the queue status, that may take some time.
		/// 
		///  The method will block until the queue is empty.
		/// 
		///  This method is thread-safe.
		/// </summary>
		public static void Shutdown()
		{
			lock (padlock)
			{
				if (Client != null)
				{
					Client.Shutdown();
					Client = null;
				}
			}
		}

	}
}
