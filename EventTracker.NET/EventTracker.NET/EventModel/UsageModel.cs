using System;

namespace SquidSolutions
{
	/// <summary>
	/// Usage event is just a generic collection of properties that can be integrated in various type of events
	/// </summary>
	public class UsageModel : EventModel
	{

		/// <summary>
		/// this is the client IP (v4 or v6)
		/// </summary>
		public static string UsageClientIP = "ux:clientIP";

		/// <summary>
		/// this is a persistent ID with session scope, that can be used to group events in the same session. 
		/// It can be a server-side session identification, or client-side cookie
		/// </summary>
		public static string UsageSessionID = "ux:sessionID";
	
		/// <summary>
		/// this is an internal ID that possibly identify uniquely the user
		/// </summary>
		public static string UsageUserID = "ux:userID";

		///  <summary>
     	/// this is the full referrer’s URL
		/// </summary>
		public static string UsageReferrer = "ux:referrer";

		/// <summary>
		/// this is the full URL (domain, path, query string + anchors ...) requested
		/// </summary>
		public static string UsagePageViewURL = "ux:pageViewURL";

		/// <summary>
		/// this is a persistent ID that can span multiple application server. 
		/// It can be used to correlate events submitted from different systems but used to fulfill the same user request
		/// </summary>
		public static string UsageTransactionID = "ux:transactionID";

		/// <summary>
		/// HTTP return code send back by the server to the client
		/// </summary>
		public static string UsageHttpReturnCode = "ux:httpReturnCode";

		/// <summary>
		/// Any error text that would be interesting to report, send back to the client from the server
		/// </summary>
		public static string UsageErrorCode = "ux:errorCode";

		public UsageModel(string schemaName) : base(SchemaName) {
		}

		public UsageModel(string schemaName, string eventType) : base(schemaName,eventType) {
		}

		/// <summary>
		/// the clientIP
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="IP">the client IP</param>
		public UsageModel WithClientIP(string IP) {
			base.Add(UsageClientIP, IP);
			return this;
		}

		/// <summary>
		/// the session identification
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="ID">the session ID</param>
		public UsageModel WithSessionID(String ID) {
			base.Add(UsageSessionID, ID);
			return this;
		}

		/// <summary>
		///the user identification if it is not anonymous
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="ID">the user ID</param>
		public UsageModel WithUserID(String ID) {
			base.Add(UsageUserID, ID);
			return this;
		}

		/// <summary>
		///  this is the full referrer’s URL
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="URL">URL.</param>
		public UsageModel WithReferrerURL(string URL) {
			base.Add(UsageReferrer,URL);
			return this;
		}

		/// <summary>
		/// the page view by the user, associated with this event
		/// </summary>
		/// <returns>t</returns>his Usage event
		/// <param name="url">the URL</param>
		public UsageModel WithPageViewURL(String url) {
			base.Add(UsagePageViewURL, url);
			return this;
		}

		/// <summary>
		/// this is a persistent ID that can span multiple application server. 
		/// It can be used to correlate events submitted from different systems but used to fulfill the same user request
		/// </summary>
		/// <returns>this usage event</returns>
		/// <param name="ID">transaction ID</param>
		public UsageModel WithTransactionID(String ID) {
			base.Add(UsageTransactionID, ID);
			return this;
		}

		/// <summary>
		/// HTTP return code send back by the server to the client
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="returnCode">the HTTP return code</param>
		public UsageModel WithHttpReturnCode(int returnCode) {
			base.Add(UsageHttpReturnCode,returnCode);
			return this;
		}

		/// <summary>
		/// Any error text that would be interesting to report, send back to the client from the server
		/// </summary>
		/// <returns>this Usage event</returns>
		/// <param name="error">the error code if exists</param>
		public UsageModel WithErrorCode(String error) {
			base.Add(UsageErrorCode,error);
			return this;
		}

	}
}

