using System;

namespace SquidSolutions
{
	public class Config
	{
	
		public String Endpoint { get; set; }

		public String AppKey { get; set; }

		public String SecretKey { get; set; }

		public int MaxFlusherCount { get; set; }

		public int QueueLimit { get; set; }

		public int BatchSize { get; set; }

		public int SendTimeout { get; set; }

		public Config (String appKey, String secretKey)
		{
			this.Endpoint = DefaultConfig.endpoint;
			this.MaxFlusherCount = DefaultConfig.maxFlusherCount;
			this.QueueLimit = DefaultConfig.queueLimit;
			this.BatchSize = DefaultConfig.batchSize;
			this.SendTimeout = DefaultConfig.sendTimeout;
			//
			this.AppKey = appKey;
			this.SecretKey = secretKey;
		}

	}
}

