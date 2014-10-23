using NUnit.Framework;
using System;
using SquidSolutions;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Concurrent;
using System.Web;

namespace Test
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCaseJSON ()
		{
			EventModel test = new EventModel ("schema", "event");
			JsonSerializerSettings settings = new JsonSerializerSettings() {
				// Converters = new List<JsonConverter> { new ContextSerializer() }
			};
			string json = JsonConvert.SerializeObject(test, settings);
			Console.WriteLine (json);
		}
			
		[Test ()]
		public void TestCaseEventModel ()
		{
			UsageModel usage = new UsageModel ("hhh", "kkk")
				.WithClientIP ("ip")
				.WithErrorCode ("null")
				.WithHttpReturnCode (202)
				.WithPageViewURL ("http://")
				.WithSessionID ("uuu")
				.WithTransactionID ("yyy")
				.WithUserID ("user");
		}

		[Test ()]
		public void TestCasePublisher() {
			Config conf = new Config ("test", "test");
			conf.QueueLimit = 100;
			Publisher pub = new Publisher (conf);
			Flusher flusher = new Flusher (pub);
			flusher.Start ();
			for (int i = 0; i <= 10*conf.QueueLimit; i++) {
				SessionModel Event = new StartSessionEvent();
				Event
					.WithBrowserUUID ("123")
					.WithAccountID ("myUniversity")
					.WithAuthenticationMethod ("IPRANGE")
					.WithSessionID ("1234");
				Assert.IsTrue(pub.Send (Event),"failed at iteventration #"+i);
			}
			flusher.Shutdown ();
			Assert.AreEqual(0,pub.Count ());
		}

		[Test ()]
		public void TestCaseEventTracker()
		{
			Config conf = new Config ("test", "test");
			EventTracker.Initialize (conf);
			for (int i = 0; i <= 100; i++) {
				SessionModel Event = new StartSessionEvent();
				Event
					.WithBrowserUUID ("123")
					.WithAccountID ("myUniversity")
					.WithAuthenticationMethod ("IPRANGE")
					.WithSessionID ("1234");
				EventTracker.Send (Event);
			}
			EventTrackerClient client = EventTracker.Client;
			EventTracker.Shutdown ();
			Stats stats = client.getStats ();
			Assert.IsNotNull (stats);
			Assert.AreEqual (0, stats.QueueSize);
		}

		[Test ()]
		public void TestCaseConnection()
		{
			Config conf = new Config ("squid-test", "9ff7b38a3d6a45f1a7db0c5e12161b3f");
			conf.Endpoint = "http://localhost:8080/tracker/api/v1.0";
			EventTracker.Initialize (conf);
			SessionModel Event = new StartSessionEvent();
			Event
				.WithBrowserUUID ("123")
				.WithAccountID ("myUniversity")
				.WithAuthenticationMethod ("IPRANGE")
				.WithSessionID ("1234");
			EventTracker.Send (Event);
			EventTrackerClient client = EventTracker.Client;
			EventTracker.Shutdown ();
			Stats stats = client.getStats ();
			Assert.IsNotNull (stats);
			Assert.AreEqual (0, stats.QueueSize);
			Assert.AreEqual (1, stats.Succeed);
			Assert.AreEqual (0, stats.Failed);
		}
			
		[Test ()]
		public void TestCaseBlockingCollection ()
		{
			BlockingCollection<string> collection = new BlockingCollection<string>(100);
			collection.TryAdd ("1");
			collection.TryAdd ("2");
			string x = null;
			collection.TryTake (out x);
			collection.TryTake (out x);
		}

		[Test ()]
		public void TestCaseHTTP ()
		{

			Stopwatch watch = new Stopwatch();

			try
			{

				string client = "http://default-environment-3hj9nxa3pq.elasticbeanstalk.com/api/v1.0";
				Uri uri = new Uri(HttpUtility.UrlEncode(client));

				// set the current request time
				//batch.SentAt = DateTime.Now.ToString("o");

				//string json = JsonConvert.SerializeObject(batch, settings);

				HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);

				// Basic Authentication
				// https://segment.io/docs/tracking-api/reference/#authentication
				//request.Headers["Authorization"] = BasicAuthHeader(batch.WriteKey, "");

				request.Timeout = 6000;
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

				watch.Start();

				using (var requestStream = request.GetRequestStream())
				{
					using (StreamWriter writer = new StreamWriter(requestStream))
					{
						writer.Write("test");
					}
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					watch.Stop();

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
				}
			}
			catch (WebException e) 
			{
				watch.Stop();
				//Fail(batch, ParseException(e), watch.ElapsedMilliseconds);
			}
			catch (System.Exception e)
			{
				watch.Stop();
				//Fail(batch, e, watch.ElapsedMilliseconds);
			}
		}
	}
}

