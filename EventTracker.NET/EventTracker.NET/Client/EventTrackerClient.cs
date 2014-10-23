using System;
using System.Collections.Generic;

namespace SquidSolutions.EventTracker.Client
{
	public class EventTrackerClient
	{

		internal Publisher Publisher{ get; set; }
		internal List<Flusher> Flushers;

		public EventTrackerClient (Config config)
		{
			this.Publisher = new Publisher(config);
			Flushers = new List<Flusher>();
			int flusherCount = Math.Max(1, config.MaxFlusherCount);
			for (int i=0;i<flusherCount;i++) {
				Flusher flusher = new Flusher(this.Publisher);
				Flushers.Add(flusher);
				flusher.Start();
			}
		}
			
		public Boolean IsRunning() {
			return this.Publisher.IsRunning();
		}

		public Stats getStats() {
			if (this.Publisher!=null) {
				return this.Publisher.getStats ();
			} else {
				return null;
			}
		}

		public void Send(EventModel eventModel) {
			Publisher.Send(eventModel);
		}

		/// <summary>
		/// Properly shutdown the client, taking care to flush the queue.
		/// The client will stop accepting events right away; then it will stop each flusher; 
		/// then it will flush the queue.
		///
		/// The shutdown method will return when all the events are safely flushed.
		/// </summary>
		public void Shutdown() {
			if (Publisher.IsRunning()) {
				Publisher.Shutdown();// stop accepting new events
				foreach (Flusher Flusher in Flushers) {
					Flusher.Shutdown();
				}
				Publisher.Flush();
			}
		}
	}
}

