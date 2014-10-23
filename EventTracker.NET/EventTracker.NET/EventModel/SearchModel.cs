using System;
using System.Collections.Generic;

namespace SquidSolutions.EventTracker
{
	/// <summary>
	/// Properties to track search events. It extends from the Account model.
	/// </summary>
	public class SearchModel : AccountModel
	{
	
		public static string searchSchema = "search:pub_1.0";

		///
     	/// search event = result of a search query
     	/// 
		public static string searchEventType = "search";

		///
     	/// Search term entered by the user
     	/// 
		public static string SearchTerms = "sx:terms";

		///
     	/// Any set of key/value pairs that identify a filter & options (multiple choice supported) selected to filter the search results
     	/// 
		public static string SearchFilters = "sx:filters";

		///
     	/// Type of search engine used to resolve the search
     	/// example: quick/basic, advanced, ...
     	/// 
		public static string SearchEngine = "sx:engine";

		///
     	/// Total number of search results
     	/// 
		public static string SearchResultCount = "sx:resultCount";

		///
     	/// Page number currently displayed. Should be 1 for the first result page, then 2 if the user click next. 
     	/// You can use the SearchResultID to group several pages within the same search.
     	/// 
		public static string SearchResultPage = "sx:resultPage";

		/// <summary>
		/// The search result products
		/// </summary>
		public static string SearchResultProducts = "sx:resultProducts";

		///
     	/// UUID associated with the original search event. 
     	/// Can be used to link event initiated from the search result page, for example going to next page or reference retrieval.
     	/// 
		public static string SearchResultUUID = "sx:resultUUID";

		protected SearchModel(string schemaName, string eventType) : base(schemaName, eventType) {
		}

		protected SearchModel(string eventType) : base(searchSchema, eventType) {
		}

		/// <summary>
		/// Search term entered by the user
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">Terms.</param>
		public SearchModel WithTerms(string terms) {
			base.Add(SearchTerms, terms);
			return this;
		}

		/// <summary>
		/// Any set of key/value pairs that identify a filter & options (multiple choice supported) selected to filter the search results
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">filters.</param>
		public SearchModel WithFilters(string filters) {
			base.Add(SearchFilters, filters);
			return this;
		}

		/// <summary>
		/// Type of search engine used to resolve the search
		/// example: quick/basic, advanced, ...
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">engine.</param>
		public SearchModel WithEngine(string engine) {
			base.Add(SearchEngine, engine);
			return this;
		}

		/// <summary>
		/// UUID associated with the original search event. 
		/// Can be used to link event initiated from the search result page, for example going to next page or reference retrieval.
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">UUID.</param>
		public SearchModel WithResultID(string UUID) {
			base.Add(SearchResultUUID, UUID);
			return this;
		}

		/// <summary>
		/// Total number of search results
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">count.</param>
		public SearchModel WithResultCount(int count) {
			base.Add(SearchResultCount, count);
			return this;
		}

		/// <summary>
		/// Page number currently displayed. Should be 1 for the first result page, then 2 if the user click next. 
		/// You can use the SearchResultID to group several pages within the same search.
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">pageNumber.</param>
		public SearchModel WithResultPage(int pageNumber) {
			base.Add(SearchResultPage , pageNumber);
			return this;
		}

		/// <summary>
		/// the list of products associated with this search
		/// </summary>
		/// <returns>this Search model</returns>
		/// <param name="terms">list of products.</param>
		public SearchModel WithResultProducts(List<string> products) {
			base.Add(SearchResultProducts , products);
			return this;
		}

	}
}

