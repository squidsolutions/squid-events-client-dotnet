using System;

namespace SquidSolutions
{

	/// <summary>
	/// an event model for managing Account specific properties
	/// </summary>
	public class AccountModel : UsageModel
	{
	
		/// <summary>
		/// this is an internal ID of the user account. 
		/// It must allow to retrieve account reference data.
		/// </summary>
		public static string PubAccountID = "pub:accountID";

		/// <summary>
		/// this is a reference code that define how the user gain access to the Account
		/// </summary>
		public static string PubAuthenticationMethod = "pub:authMethod";


		public AccountModel (string schemaName) : base(schemaName)
		{
		}

		public AccountModel (string schemaName, string eventType) : base(schemaName,eventType)
		{
		}
			
		/// <summary>
		/// this is an internal ID of the user account. 
		/// It must allow to retrieve account reference data.
		/// </summary>
		/// <returns>this Account event</returns>
		/// <param name="accountID">Account ID</param>
		public AccountModel WithAccountID(string accountID) {
			base.Add(PubAccountID, accountID);
			return this;
		}

		/// <summary>
		/// this is a reference code that define how the user gain access to the Account
		/// </summary>
		/// <returns>this Account event</returns>
		/// <param name="methodID">methodID</param>
		public AccountModel WithAuthenticationMethod(string methodID) {
			base.Add(PubAuthenticationMethod, methodID);
			return this;
		}

	}
}

