using System;

namespace SquidSolutions.EventTracker
{
	/// <summary>
	/// construct a start session event
	/// </summary>
	public class StartSessionEvent : SessionModel
	{
		public StartSessionEvent () : base(SessionStartEventType)
		{
		}
	}
}

