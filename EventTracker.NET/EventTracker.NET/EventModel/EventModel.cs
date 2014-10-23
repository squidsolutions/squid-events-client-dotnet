using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SquidSolutions
{
	/*
	 * The base event model class. It defines basic information common to every events.
	 */
	public class EventModel : Dictionary<String,Object>
	{

		// the schemaName must be a reference identifier
		public static string SchemaName = "xx:schemaName";

		// the type of event, must be a reference
		public static string EventType = "xx:eventType";

		// the date of the event; this one will set both the time and time-zone
		public static string EventDate = "xx:eventDate";

		// the date timezone if not GMT-0
		public static string EventTimeZone = "xx:eventTimeZone";

		// the serverIP; this is optional, the Tracker Server can add this latter
		public static string ServerIP = "xx:serverIP";


		public EventModel(string schemaName) {
			base.Add(SchemaName,schemaName);
		}

		public EventModel(string schemaName, string eventType) {
			base.Add(SchemaName,schemaName);
			base.Add(EventType,eventType);
			WithEventDate(DateTime.Now);
		}

		public EventModel with(string key, Object value) {
			base.Add (key, value);
			return this;
		}

		//
		// get the key value as a String. If not defined, return n.ull
		//
		public string getAsString(string key) {
			Object value = base [key];
			return value != null ? value.ToString() : null;
		}

		/// <summary>
		/// the schemaName must be a reference identifier
		/// </summary>
		/// <returns>this event</returns>
		/// <param name="schemaName">t</param>he schema reference
		public EventModel WithSchemaName(string schemaName) {
			base.Add(SchemaName, schemaName);
			return this;
		}

		/// <summary>
		/// the type of event, must be a reference
		/// </summary>
		/// <returns>this event</returns>
		/// <param name="eventType">the event type</param>
		public EventModel WithEventType(string eventType) {
			base.Add(EventType, eventType);
			return this;
		}
			
		/// <summary>
		/// the date of the event; this one will only set the time
		/// </summary>
		/// <returns>The event date.</returns>
		/// <param name="date">Date.</param>
		public EventModel WithEventDate(DateTime date) {
			base.Add(EventDate, date.Ticks);
			return this;
		}

		/// <summary>
		/// the date of the event in millisecond since Epoch
		/// </summary>
		/// <returns>The event date.</returns>
		/// <param name="date">Date.</param>
		public EventModel WithEventDate(long date) {
			base.Add(EventDate, date);
			return this;
		}

		/// <summary>
		/// the date timezone if not GMT-0
		/// </summary>
		/// <returns>The event time zone.</returns>
		/// <param name="tmz">Tmz.</param>
		public EventModel WithEventTimeZone(TimeZone tmz) {
			base.Add(EventTimeZone, tmz.StandardName);
			return this;
		}

		/// <summary>
		/// the serverIP; this is optional, the Tracker Server can add this latter
		/// </summary>
		/// <returns>The server I.</returns>
		/// <param name="IP">I.</param>
		public EventModel WithServerIP(string IP) {
			base.Add(ServerIP, IP);
			return this;
		}
	}
}

