using NUnit.Framework;
using System;
using SquidSolutions;
using SquidSolutions.EventTracker;
using SquidSolutions.EventTracker.Client;

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
		public void TestCaseEventTracker()
		{
			Config conf = new Config ("squid-test", "9ff7b38a3d6a45f1a7db0c5e12161b3f");
			conf.MaxFlusherCount = 5;
			conf.Endpoint = "http://localhost:8080/tracker/api/v1.0";
			conf.SendTimeout = 0;
			EventTracker.Initialize (conf);
			Stopwatch watch = new Stopwatch ();
			watch.Start ();
			int count = 10000;
			int total = 1000 * 3;
			for (int i = 0; i < count; i++) {
				EventTracker.Send (CreateSessionEvent());
				EventTracker.Send (CreateSearchEvent());
				EventTracker.Send (CreateRetrievalEvent());
			}
			watch.Stop ();
			Console.Out.WriteLine ("sending events " + watch.ElapsedMilliseconds + "ms for " + total + " events");
			EventTrackerClient client = EventTracker.Client;
			watch.Restart ();
			EventTracker.Shutdown ();
			watch.Stop ();
			Console.Out.WriteLine ("flushing events " + watch.ElapsedMilliseconds + "ms for " + total + " events");
			Stats stats = client.getStats ();
			Assert.IsNotNull (stats);
			Assert.AreEqual (0, stats.QueueSize);
			Assert.AreEqual (total, stats.Succeed);
			Assert.AreEqual (0, stats.Failed);
		}

		[Test ()]
		public void TestCaseConnection()
		{
			Config conf = new Config ("squid-test", "9ff7b38a3d6a45f1a7db0c5e12161b3f");
			conf.Endpoint = "http://localhost:8080/tracker/api/v1.0";
			EventTracker.Initialize (conf);
			EventTracker.Send (CreateSessionEvent());
			EventTracker.Send (CreateSearchEvent());
			EventTracker.Send (CreateRetrievalEvent());
			EventTrackerClient client = EventTracker.Client;
			EventTracker.Shutdown ();
			Stats stats = client.getStats ();
			Assert.IsNotNull (stats);
			Assert.AreEqual (0, stats.QueueSize);
			Assert.AreEqual (3, stats.Succeed);
			Assert.AreEqual (0, stats.Failed);
		}

		public EventModel CreateSessionEvent() {
			EventModel Event = new StartSessionEvent ()
				.WithBrowserUUID ("123")
				.WithUserAgent ("chrome")
				.WithAccountID ("myUniversity")
				.WithAuthenticationMethod ("IPRANGE")
				.WithReferrerURL ("http://google.com")
				.WithHttpReturnCode (202)
				.WithPageViewURL ("http://myDomain.com/landing_page.html")
				.WithSessionID ("1234")
				.WithUserID ("Tom");
			return Event;
		}

		public EventModel CreateSearchEvent() {
			EventModel Event = new SearchEvent ()
				.WithTerms("dotnet framework macos")
				.WithFilters("filter1=value1;filter2=value2")
				.WithResultCount(100)
				.WithResultPage(1)
				.WithResultID("search1")
				.WithAccountID ("myUniversity")
				.WithAuthenticationMethod ("IPRANGE")
				.WithReferrerURL ("http://google.com")
				.WithHttpReturnCode (202)
				.WithPageViewURL ("http://myDomain.com/landing_page.html")
				.WithSessionID ("1234")
				.WithUserID ("Tom");
			return Event;
		}
			
		public EventModel CreateRetrievalEvent() {
			EventModel Event = new RetrievalEvent ()
				.WithContentOwnerID("parentUniversity")
				.WithEntitlement("demo")
				.WithContentReferenceArticle(
					new ArticleModel()
					.WithPublicationTitle("Advances in Database Technology — EDBT'98")
					.WithReferenceSource("scopus")
					.WithReferenceSourceType("web")
					.WithLanguage("us")
					.WithContentType("article")
					.WithDOI("10.1007/BFb0101000")
					.WithISBN("978-3-540-69709-1")
					.WithISSN("0302-9743")
					.WithDBID("springer")
				)
				.WithDisplayFormat("PDF")
				.WithSearchOriginID("search1")
				.WithAccountID ("myUniversity")
				.WithAuthenticationMethod ("IPRANGE")
				.WithReferrerURL ("http://google.com")
				.WithHttpReturnCode (202)
				.WithPageViewURL ("http://myDomain.com/landing_page.html")
				.WithSessionID ("1234")
				.WithUserID ("Tom");
			return Event;
		}

			
	}
}

