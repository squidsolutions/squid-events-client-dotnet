using System;

namespace SquidSolutions
{

	/// <summary>
	/// This event is created when a user performs a search in the application. 
	/// It allows to define the search parameter and also to collect information regarding the search results.
	///
	/// This event includes properties from the following models:
	/// <li>Search Model
	/// <li>Account Model
	/// <li>Usage Model  
	/// </summary>
	public class SearchEvent : SearchModel
	{
		public SearchEvent () : base(searchEventType)
		{
		}

	}
}

