using System;

namespace SquidSolutions.EventTracker
{
	public class ArticleModel : EventModel
	{
	
		public static string ArticleSchemaName = "art:pub_1.0";

		/// <summary>
		/// type of content that has been retrieved.
		/// example: Dissertation, Newspapers, Book...
		/// </summary>
		public static string ArticleContentType = "art:contentType";

		/// <summary>
		/// International code of the language in which the content is made.
		/// </summary>
		public static string ArticleLanguage = "art:language";

		/// <summary>
		/// Academic subject matter the content relates to
		/// </summary>
		public static string ArticleDiscipline = "art:discipline";

		/// <summary>
		/// Name of the source from which the reference was imported
		/// </summary>
		public static string ArticleReferenceSource = "art:source";

		/// <summary>
		/// Name of category of grouping of reference sources
		/// </summary>
		public static string ArticleReferenceSourceType = "art:sourceType";

		/// <summary>
		/// Name of the journal the article was published in
		/// </summary>
		public static string ArticlePublicationTitle = "art:pubTitle";

		/// <summary>
		/// Identification of the database the journal belongs to
		/// </summary>
		public static string ArticleDBID = "art:dbid";

		/// <summary>
		/// When applicable: ISSN if the article is published in a periodical
		/// </summary>
		public static string ArticleISSN = "art:issn";

		/// <summary>
		/// When applicable: ISBN if the article is published in a book
		/// </summary>
		public static string ArticleISBN = "art:isbn";

		/// <summary>
		/// When applicable: Digital Object Identifier of the article
		/// </summary>
		public static string ArticleDOI = "art:doi";

		/// <summary>
		/// Internal ID of the article in your database
		/// </summary>
		public static string ArticleCustomID = "art:customID";

		/// <summary>
		/// default constructor
		/// </summary>
		public ArticleModel() : base(ArticleSchemaName) {
		}

		protected ArticleModel(string schemaName) : base(schemaName) {
		}

		protected ArticleModel(string schemaName, string eventType) : base(schemaName, eventType) {
		}

		/// <summary>
		/// type of content that has been retrieved.
		/// example: Dissertation, Newspapers, Book...
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="contentType">Content type</param>
		public ArticleModel WithContentType(string contentType) {
			base.Add(ArticleContentType,contentType);
			return this;
		}

		/// <summary>
		/// International code of the language in which the content is made.
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="language">Language</param>
		public ArticleModel WithLanguage(string language) {
			base.Add(ArticleLanguage,language);
			return this;
		}

		/// <summary>
		/// Academic subject matter the content relates to
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="discipline">Discipline.</param>
		public ArticleModel WithDiscipline(string discipline) {
			base.Add(ArticleDiscipline,discipline);
			return this;
		}

		/// <summary>
		/// Name of the source from which the reference was imported
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="source">Source.</param>
		public ArticleModel WithReferenceSource(string source) {
			base.Add(ArticleReferenceSource,source);
			return this;
		}

		/// <summary>
		/// Name of category of grouping of reference sources
		/// </summary>
		/// <returns>t</returns>is Article model
		/// <param name="type">Type.</param>
		public ArticleModel WithReferenceSourceType(string type) {
			base.Add(ArticleReferenceSourceType,type);
			return this;
		}

		/// <summary>
		/// Name of the journal the article was published in
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="title">Title.</param>
		public ArticleModel WithPublicationTitle(string title) {
			base.Add(ArticlePublicationTitle,title);
			return this;
		}

		/// <summary>
		/// Identification of the database the journal belongs to
		/// </summary>
		/// <returns>the Article model</returns>
		/// <param name="dbid">Dbid.</param>
		public ArticleModel WithDBID(string dbid) {
			base.Add(ArticleDBID,dbid);
			return this;
		}

		/// <summary>
		/// When applicable: ISSN if the article is published in a periodical
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="issn">Issn.</param>
		public ArticleModel WithISSN(string issn) {
			base.Add(ArticleISSN,issn);
			return this;
		}

		/// <summary>
		/// When applicable: ISBN if the article is published in a book
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="isbn">Isbn.</param>
		public ArticleModel WithISBN(string isbn) {
			base.Add(ArticleISBN,isbn);
			return this;
		}

		/// <summary>
		/// When applicable: Digital Object Identifier of the article
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="doi">Doi.</param>
		public ArticleModel WithDOI(string doi) {
			base.Add(ArticleDOI,doi);
			return this;
		}

		/// <summary>
		/// Internal ID of the article in your database
		/// </summary>
		/// <returns>this Article model</returns>
		/// <param name="customID">Custom I.</param>
		public ArticleModel WithCustomID(string customID) {
			base.Add(ArticleCustomID,customID);
			return this;
		}
	}
}

