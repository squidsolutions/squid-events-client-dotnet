using System;

namespace SquidSolutions.EventTracker
{

	/// <summary>
	/// Properties to track retrieval events. It extends from the Account model.
	/// </summary>
	public class RetrievalModel : AccountModel
	{
	
		public static string RetrievalSchema = "retrieval:pub_1.0";

		public static string RetrievalDisplayEventType = "display";

		/// <summary>
		/// identify the search that leads to that display, see SearchresultID
		/// </summary>
		public static string RetrievalSearchOriginID = "rt:searchOriginID";

		/// <summary>
		/// the reference of the content. Must be a valid reference in the meta-data source.
		/// </summary>
		public static string RetrievalContentReferenceID = "rt:contentRefID";

		/// <summary>
		/// if the content is not referenced, this allow to provide a ArticleModel description
		/// </summary>
		public static string RetrievalContentReferenceArticle = "rt:contentRefArticle";

		/// <summary>
		/// the content type of the artifact retrieved. It should be a reference value.
		/// </summary>
		public static string RetrievalContentType = "rt:contentType";

		/// <summary>
		/// reference the display format
		/// </summary>
		public static string RetrievalDisplayFormat = "rt:displayFormat";

		/// <summary>
		///  identify the accountID that owns the content for the actual viewer
		/// </summary>
		public static string RetrievalContentOwner = "rt:contentOwner";

		/// <summary>
		/// define the entitlement for that account/display
		/// </summary>
		public static string RetrievalContentEntitlement = "rt:contentEntitlement";

		protected RetrievalModel(String schemaName, String eventType) : base(schemaName, eventType) {
		}

		protected RetrievalModel(String eventType) : base(eventType) {
		}

		/// <summary>
		/// identify the search that leads to that display, see SearchresultID
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">searchResultID</param>
		public RetrievalModel WithSearchOriginID(String searchResultID) {
			base.Add(RetrievalSearchOriginID,searchResultID);
			return this;
		}

		/// <summary>
		/// the reference of the content. Must be a valid reference in the meta-data source.
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">contentReferenceID</param>
		public RetrievalModel WithContentReferenceID(String contentReferenceID) {
			base.Add(RetrievalContentReferenceID,contentReferenceID);
			return this;
		}
			
		/// <summary>
		/// if the content is not referenced, this allow to provide a ArticleModel description
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">the Article model</param>
		public RetrievalModel WithContentReferenceArticle(ArticleModel article) {
			base.Add(RetrievalContentReferenceArticle,article);
			return this;
		}
			
		/// <summary>
		/// the content type of the artifact retrieved
		/// example: ID of an image, video, article, journal…
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">contentType</param>
		public RetrievalModel WithContentType(String contentType) {
			base.Add(RetrievalContentType,contentType);
			return this;
		}
			
		/// <summary>
		/// the display format value
		/// example: JPEG, HTML, ABSTRACT, PDF
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">displayFormat</param>
		public RetrievalModel WithDisplayFormat(String displayFormat) {
			base.Add(RetrievalDisplayFormat,displayFormat);
			return this;
		}

		/// <summary>
		/// the account ID that owns the content.
		/// It must be a valid reference in the meta-data source.
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID"contentOwnerID></param>
		public RetrievalModel WithContentOwnerID(String contentOwnerID) {
			base.Add(RetrievalContentOwner,contentOwnerID);
			return this;
		}
			
		/// <summary>
		/// the entitlement rule for providing access to that content
		/// </summary>
		/// <returns>this Retrieval model</returns>
		/// <param name="ID">entitlement</param>
		public RetrievalModel WithEntitlement(String entitlement) {
			base.Add(RetrievalContentEntitlement,entitlement);
			return this;
		}
	}
}

