using System;
using SQLite;
namespace NewsApp
{
    public class DBNewsArticle
    {
        [PrimaryKey, AutoIncrement]
        public string ID { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public string SourceID { get; set; }
        public string SourceName { get; set; }

        public int ClusterNumber { get; set; }

        public DBNewsArticle()
        {
            // Do nothing - allows for a DBNewsArticle database
        }

        public DBNewsArticle(NewsArticle article, int clusterNumber)
        {
            Title = article.Title;
            Description = article.Description;
            Url = article.Url;
            UrlToImage = article.UrlToImage;
            SourceID = article.Source.ID;
            SourceName = article.Source.Name;
            ClusterNumber = clusterNumber;
        }
    }
}
