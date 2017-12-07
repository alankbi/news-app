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

		public override int GetHashCode()
		{
			return Url.GetHashCode();
		}
    }
}
