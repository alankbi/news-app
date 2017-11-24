using System;
namespace NewsApp
{
    public class NewsArticle
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public NewsSource source { get; set; }

		public override int GetHashCode()
		{
			return url.GetHashCode();
		}
    }
}
