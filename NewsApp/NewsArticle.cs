using System;
namespace NewsApp
{
    public class NewsArticle
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public NewsSource Source { get; set; }

        public NewsArticle()
        {
            // Do nothing - allows for JSON parsing in ArticleFetcher
        }

        // For converting types after retrieving from database
        public NewsArticle(DBNewsArticle article)
        {
            Title = article.Title;
            Description = article.Description;
            Url = article.Url;
            UrlToImage = article.UrlToImage;
            Source = new NewsSource();
            Source.ID = article.SourceID;
            Source.Name = article.SourceName;
        }

		public override int GetHashCode()
		{
			return Url.GetHashCode();
		}
    }
}
