using System;

namespace SquidSolutions
{
	public class DefaultConfig
	{
	
		public static  String endpoint = "https://events.tracker.squidanalytics.com/tracker/api/v1.0";

		public static int maxFlusherCount = 1;

		public static int queueLimit = 10000;

		public static int batchSize = 100;

		// default timeout for the send() method is 10ms
		public static int sendTimeout = 10;
	}
}

