using System;

namespace SquidSolutions
{
	public class Stats
	{

		/// <summary>
		/// the number of events sitting in the queue
		/// </summary>
		/// <value>The size of the queue.</value>
		public int QueueSize { get; set; }

		/// <summary>
		/// number of succesfull attempts
		/// </summary>
		/// <value>The succeed.</value>
		public long Succeed { get; set; }

		/// <summary>
		/// number of failed attemps
		/// </summary>
		/// <value>The failed.</value>
		public long Failed { get; set; }
	}
}

