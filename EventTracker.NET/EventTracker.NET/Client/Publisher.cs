using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace SquidSolutions.EventTracker.Client
{
	/// <summary>
	/// main class to publish event to a queue and batch then to the Event Tracker Server
	/// </summary>
	public class Publisher
	{

		private static int HTTP_TIMEOUT = 6000;
		private static int MAX_RETRY = 3;
	
		private bool Go = true;
		private Config Config;
		private string url = null;
		private BlockingCollection<EventModel> _queue;

		private long successful = 0;
		private long failed = 0;

		public Publisher (Config config)
		{
			CheckConfig (config);
			this.url = CheckURL (config);
			this.Config = config;
			this._queue = new BlockingCollection<EventModel> (config.QueueLimit);
		}

		/// <summary>
		/// check that the endpoint is correctly configured and that the service is up and running
		/// </summary>
		/// <returns>The UR.</returns>
		/// <param name="config">Config.</param>
		private string CheckURL(Config config) {
			if (config.Endpoint==null || config.Endpoint=="") {
				String msg = "Event Tracker client configuration error: missing the endpoint";
				throw new InvalidOperationException(msg);
			}
			string url = config.Endpoint;
			if (!url.EndsWith("/")) url += "/";
			url += Constants.BASE_SERVICE+"/v"+Constants.VERSION;
			return url;
		}

		internal void CheckConfig(Config config)
		{
			if (config.AppKey==null || config.AppKey.Length==0) {
				String msg = "Event Tracker client configuration error: missing the appKey";
				throw new InvalidOperationException(msg);
			}
			if (config.SecretKey==null || config.SecretKey.Length==0) {
				String msg = "Event Tracker client configuration error: missing the secretKey";
				throw new InvalidOperationException(msg);
			}
		}

		public Stats getStats() {
			Stats stats = new Stats ();
			stats.QueueSize = _queue.Count;
			stats.Succeed = this.successful;
			stats.Failed = this.failed;
			return stats;
		}

		/// <summary>
		/// Send the specified eventModel.
		/// </summary>
		/// <param name="eventModel">Event model.</param>
		public bool Send(EventModel eventModel) {
			if (this.Go) {
				if (Config.SendTimeout >= 0) {
					return this._queue.TryAdd (eventModel, Config.SendTimeout);
				} else {
					return this._queue.TryAdd (eventModel);
				}
			} else {
				// give some feedback here ?
				return false;
			}
		}

		/// <summary>
		/// Determines whether this instance is running.
		/// </summary>
		/// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
		public Boolean IsRunning() {
			return this.Go;
		}

		/// <summary>
		/// Flush the queue.
		/// </summary>
		public void Flush() {
			int size = _queue.Count;
			int i=0;
			while (_queue.Count>0) {
				List<EventModel> copy = new List<EventModel>(Config.BatchSize);
				for (int j=0 ;i < size && j <Config.BatchSize; i++,j++) {
					copy.Add(_queue.Take());// no need to wait
				}
				if (copy.Count>0) {
					SendBatchMessage(copy);
				}
			}
		}

		public int Count() {
			return _queue.Count;
		}

		/// <summary>
		/// Poll the queue.
		/// </summary>
		public void Poll() {
			List<EventModel> batch = new List<EventModel>(Config.BatchSize);
			while (true) {
				EventModel eventModel;
				if (_queue.TryTake(out eventModel,1000)) {// try for 1s
					batch.Add(eventModel);
					if (batch.Count>=Config.BatchSize) {
						break;//enought to push
					}
				} else {
					break;// send the batch if any, and return to the flusher loop
				}
			}
			if (batch.Count>0) {
				SendBatchMessage(batch);
			}
		}

		internal JsonSerializerSettings settings = new JsonSerializerSettings() {
			// Converters = new List<JsonConverter> { new ContextSerializer() }
		};

		internal void SendBatchMessage (List<EventModel> events)
		{
			string serialize = JsonConvert.SerializeObject(events);
			// http://www.opinionatedgeek.com/blog/BlogEntry=000361/BlogEntry.aspx
			byte[] raw = Encoding.UTF8.GetBytes(serialize);
			string data = Convert.ToBase64String(raw);
			String signature = computeSignature(data);
			Stopwatch watch = new Stopwatch ();
			watch.Start ();
			HttpStatusCode response = doPostRetry(Config.Endpoint, Config.AppKey, signature, data);
			watch.Stop ();
			Console.Out.WriteLine ("http latency = "+watch.ElapsedMilliseconds/1000.0+"s");
			if (response == HttpStatusCode.Accepted) {
				Interlocked.Add (ref this.successful, events.Count);
			} else {
				Interlocked.Add (ref this.failed, events.Count);
			}
		}

		internal string computeSignature(string data) {
			HMACSHA1 algo = new HMACSHA1 (Encoding.UTF8.GetBytes(Config.SecretKey));
			byte[] hash = algo.ComputeHash (Encoding.UTF8.GetBytes (data));
			return Convert.ToBase64String(hash);
		}

		internal HttpStatusCode doPostRetry(string endpoint, string appKey, string signature, string data) {
			int retry = 0;
			HttpStatusCode status;
			do {
				if (retry>0) Console.Out.WriteLine ("Retry x" + retry);
				status = doPost (endpoint, appKey, signature, data);
				if (status == HttpStatusCode.BadGateway) {
					retry++;
				}
			} while (status == HttpStatusCode.BadGateway && retry<MAX_RETRY);
			return status;
		}

		internal HttpStatusCode doPost(string endpoint, string appKey, string signature, string data) {
			//
			try
			{
				//
				string connection = url + "?";
				connection += Constants.APP_KEY_PARAM + "=" + HttpUtility.UrlEncode(appKey);
				connection += "&" + Constants.SIGNATURE_PARAM + "=" + HttpUtility.UrlEncode(signature);
				if (this.Config.AppVersion!=null && this.Config.AppVersion!="") {
					connection += "&" + Constants.APP_VERSION_PARAM + "=" + HttpUtility.UrlEncode(this.Config.AppVersion);
				}
				if (this.Config.AppTestFlag) {
					connection += "&" + Constants.APP_TEST_PARAM + "=1";
				}
				Uri uri = new Uri(connection);

				// set the current request time
				//batch.SentAt = DateTime.Now.ToString("o");

				//string json = JsonConvert.SerializeObject(batch, settings);

				HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);

				// Basic Authentication
				// https://segment.io/docs/tracking-api/reference/#authentication
				//request.Headers["Authorization"] = BasicAuthHeader(batch.WriteKey, "");

				request.Timeout = HTTP_TIMEOUT;
				request.ContentType = "application/json";
				request.Method = "POST";

				// do not use the expect 100-continue behavior
				request.ServicePoint.Expect100Continue = false;
				// buffer the data before sending, ok since we send all in one shot
				request.AllowWriteStreamBuffering = true;
				/*
					Logger.Info("Sending analytics request to Segment.io ..", new Dict
						{
							{ "batch id", batch.MessageId },
							{ "json size", json.Length },
							{ "batch size", batch.batch.Count }
						});
						*/

				using (var requestStream = request.GetRequestStream())
				{
					using (StreamWriter writer = new StreamWriter(requestStream))
					{
						writer.Write(data);
					}
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{

					if (response.StatusCode == HttpStatusCode.OK)
					{
						//Succeed(batch, watch.ElapsedMilliseconds);
					}
					else
					{
						string responseStr = String.Format("Status Code {0}. ", response.StatusCode);
						//responseStr += ReadResponse(response);
						//Fail(batch, new APIException("Unexpected Status Code", responseStr), watch.ElapsedMilliseconds);
					}
					return response.StatusCode;
				}
			}
			catch (WebException e) 
			{
				Console.WriteLine (e);
				//Fail(batch, ParseException(e), watch.ElapsedMilliseconds);
				return HttpStatusCode.BadRequest;
			}
			catch (System.Exception e)
			{
				Console.WriteLine (e);
				//Fail(batch, e, watch.ElapsedMilliseconds);
				return HttpStatusCode.BadRequest;
			}
		}

		public void Shutdown() {
			this.Go = false;
		}
	}
}

