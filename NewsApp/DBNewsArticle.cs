using System;
using SQLite;
namespace NewsApp
{
    /**
     * NewsArticle objects are converted into DBNewsArticle objects before being
     * inserted into database (SQLite can't store NewsArticle's Source field
     * variable). DBNewsArticle also has a ClusterNumber field variable to tell
     * which cluster the article belongs to (1, 2, or 3) after being retrieved.
     */
    [Table("NewsArticles")]
    public class DBNewsArticle
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

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

        // Convert types before inserting into database
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
