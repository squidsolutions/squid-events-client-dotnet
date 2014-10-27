using System;

namespace SquidSolutions.EventTracker
{
	public class DefaultConfig
	{
	
		public static  string endpoint = "http://events.tracker.squidanalytics.com/";

		public static int maxFlusherCount = 1;

		public static int queueLimit = 10000;

		public static int batchSize = 100;

		// default timeout for the send() method is 10ms
		public static int sendTimeout = 10;
	}
}

