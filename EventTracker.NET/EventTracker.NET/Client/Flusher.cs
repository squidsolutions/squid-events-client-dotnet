using System;
using System.Threading;

namespace SquidSolutions.EventTracker.Client
{
	public class Flusher
	{

		internal Publisher Publisher = null;
		internal Thread _Thread = null;
		internal bool Go = false;

		public Flusher (Publisher publisher)
		{
			this.Publisher = publisher;
			this._Thread = new Thread (new ThreadStart (Loop));
		}

		/// <summary>
		/// Start this flusher.
		/// </summary>
		public void Start() {
			if (!this.Go) {
				this.Go = true;
				this._Thread.Start ();
			}
		}

		/// <summary>
		/// this is the thread loop
		/// </summary>
		private void Loop() {
			while (this.Go) {
				this.Publisher.Poll ();
			}
		}

		/// <summary>
		/// Shutdown this flusher thread and wait until the flusher actually complete its undergoing taks
		/// </summary>
		public void Shutdown() {
			if (this.Go) {
				this.Go = false;
				this._Thread.Join ();
			}
		}
	}
}

