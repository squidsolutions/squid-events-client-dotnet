Events Tracking SDK for .NET
============================

This is the documentation for using the .NET Events Tracking client library. 
The client library provides you an easy way to integrate events tracking in your application.
You just have to focus on the business logic to define and push the events from your application code.
The library will handle all the work to send the events to the Tracker Server in an asynchrone & optimize way.

Getting started
---------------

## Dependencies

The SDK relies on:

* .NET 4.5
* Newtonsoft.json for json support

## Initialization

```
using SquidSoltions.EventTracker;

// create the config
Config Conf = new Config("application-key","secret-key");

// initialize the tracker
EventTracker.Initialize(Conf);
```
You can use the default configuration for all technical settings. It is production ready.
You must specify your application key and secret key. If not provided the Initialize() method wil throws an InvalidOperationException.


Tracking events
---------------

## creating an Event

The comprehensive documentation for Events is available at the https://github.com/squidsolutions/squid-events-api.

Following are several examples covering the three kind of currently supported events:
* StartSessionEvent
* SearchEvent
* RetrievalEvent

### start a new session event

This is an example of a new session event created for a visitor

```
// create the new session event
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
```

### search event

This is an example of a search event

```
// create the search event
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
```

### retrieval event

This is an exemple of a retrieval event, providing extensive article definition:

```
// create the retrieval event, with an Article definition embeded
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
```

## sending event

You can easily send the previous event.

```
// send the event and return
EventTracker.Send(event);
```

Note that the EventTracker will not make a internal copy of the event to avoid unnecessary memory allocation.
So you must not re-use the same EventModel object because this will lead to inconsistency in the flushing queue.

Flushing the queue
------------------

The queue will be automatically flushed when you shutdown the EventTracker:

```
EventTracker.Shutdown ();
```

Note that flushing the queue may take several seconds to return depending on the queue state & configuration.
The Shutdown() method will return only when the queue is empty.

How does it works?
------------------

The client library uses an internal queue in order to make sending events from the application non-blocking and very fast.
You can define a maximum latency for the send() method.

The queue is bound in order to prevent the library to starve memory in case of a communication problem.
If the queue is full, new events won't be accepted - and probably lost forever. 
You can configure the maximun queue size, default is 10000.

The client library sends the event in batches to the Tracker Server. 
This background process is asynchrone, and it will flush the queue every second or when the batch is full.

Performances & Configuration
----------------------------

We tested the client library to support heavy load (1000 events/s) under sustained period of time (10 minutes).

If your application is expected to generate such load, you would need to optimize the library configuration:

1. consider extending the queue size; the queue size is used mostly to absorb pick of usage. 
Remember that once the queue is full, call to send() will either fail (if you enforce a timeout) or block (until there is some room in the queue). The default value is 10000.
```
Config conf = new Config("appKey","secretKey");
conf.QueueLimit = 20000;// double default queue limit
``` 

2. it is possible to adjust the send() method timeout setting. By default the timeout is 10ms. 
By setting the timeout to 0ms, the send() will fail straight if the queue is full.
```
Config conf = new Config("appKey","secretKey");
conf.SendTimeout = 0;// send will fail straight if the queue is full
```

3. Consider adding more flushers; you can have multiple background threads flushing the queue in parallel.
Each flusher will contact the Tracker Server independently, thus adding more network load from your application.
A single flusher can sustain about 500 events/s.
```
Config conf = new Config("appKey","secretKey");
conf.MaxFlusherCount = 2;// run 2 flushers in parallel
```
