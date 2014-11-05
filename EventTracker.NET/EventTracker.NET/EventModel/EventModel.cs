using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SquidSolutions.EventTracker
{
	/*
	 * The base event model class. It defines basic information common to every events.
	 */
	public class EventModel : Dictionary<String,Object>
	{

		private static Int64 epoch = 621355968000000000;

		// the schemaName must be a reference identifier
		public static string SchemaName = "xx:schemaName";

		// the type of event, must be a reference
		public static string EventType = "xx:eventType";

		// the date of the event in UTC time,
		public static string EventDate = "xx:eventDate";

		// the local timezone
		public static string EventTimeZone = "xx:eventTMZ";

		// the serverIP; this is optional, the Tracker Server can add this latter
		public static string ServerIP = "xx:serverIP";


		public EventModel(string schemaName) {
			base.Add(SchemaName,schemaName);
		}

		public EventModel(string schemaName, string eventType) {
			// default to add DateEvent only for an actual event
			base.Add(SchemaName,schemaName);
			base.Add(EventType,eventType);
			WithEventDate(DateTime.UtcNow);
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
		/// the date of the event in locale time; this one will set both the locale and UTC time
		/// </summary>
		/// <returns>The event date.</returns>
		/// <param name="date">Date.</param>
		public EventModel WithEventDate(DateTime date) {
			base[EventDate] = (date.ToUniversalTime().Ticks-epoch)/10000;
			TimeZoneInfo tmz = TimeZoneInfo.Local;
			base[EventTimeZone] = tmz.IsDaylightSavingTime(date)?tmz.DaylightName:tmz.StandardName;
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

