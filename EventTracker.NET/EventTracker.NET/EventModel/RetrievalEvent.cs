using System;

namespace SquidSolutions.EventTracker
{
	/// <summary>
	/// construct a retrieval event
	/// </summary>
	public class RetrievalEvent : RetrievalModel
	{
		public RetrievalEvent () : base(RetrievalDisplayEventType)
		{
		}
	}
}

