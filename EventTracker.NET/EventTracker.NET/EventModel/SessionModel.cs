using System;

namespace SquidSolutions.EventTracker
{

	/// <summary>
	/// Properties to track session level events. It extends from the Account model.
	/// </summary>
	public class SessionModel : AccountModel
	{

		///
     	/// the session schema defines properties associated with a new session
     	/// The sessionID may be use further to link events occuring in the scope of this session
     	///
		public static string SessionSchema = "session_1.0";

		public static string SessionStartEventType = "start";

		///
     	/// this is a persistent UUID associated with the browser (or rich application) generating the event on the client side. 
     	/// Usually it will be retrieve from a cookie.
     	/// It can be used to group multiple session with the same browser
     	/// 
		public static string SessionBrowserID = "ss:browserUUID";

		///
     	/// this is the full referrer’s URL
     	/// 
		public static string SessionReferrer = "ss:referrer";

		///
     	/// UserAgent string sent back by the client
     	/// 
		public static string SessionUserAgent = "ss:userAgent";

		protected SessionModel(string schemaName, string eventType) : base(schemaName, eventType) {
		}

		protected SessionModel(string eventType) : base(SessionSchema, eventType) {
		}
			
		/// <summary>
		/// this is a persistent UUID associated with the browser (or rich application) generating the event on the client side. 
		/// Usually it will be retrieve from a cookie.
		/// It can be used to group multiple session originating from the same browser
		/// </summary>
		/// <param name="UUID">UUID.</param>
		public SessionModel WithBrowserUUID(string UUID) {
			base.Add(SessionBrowserID,UUID);
			return this;
		}

		/// <summary>
		/// UserAgent string sent back by the client
		/// </summary>
		/// <returns>The user agent.</returns>
		/// <param name="agent">Agent.</param>
		public SessionModel WithUserAgent(string agent) {
			base.Add(SessionUserAgent,agent);
			return this;
		}
	}
}

