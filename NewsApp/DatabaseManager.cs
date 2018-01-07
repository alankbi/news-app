using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
namespace NewsApp
{
    public class DatabaseManager
    {
        private string dbPath;
        private SQLiteConnection conn;

        private string sourcesDbPath;
        private SQLiteConnection sourcesConn;

        public bool FirstTime { get; private set; }

        public DatabaseManager()
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Clusters");
            sourcesDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Sources");
            FirstTime = !File.Exists(dbPath); // If this is the first time the app has launched

            try
            {
                conn = new SQLiteConnection(dbPath);
                conn.CreateTable<DBNewsArticle>(); // Only if doesn't exist

                sourcesConn = new SQLiteConnection(sourcesDbPath);
                sourcesConn.CreateTable<NewsSource>();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * Deletes old articles and adds new ones to the database
         */
        public void AddNewClusters(Cluster[] clusters)
        {
            // Delete old ones then add new ones
            conn.Query<DBNewsArticle>("DELETE FROM NewsArticles");

            for (int i = 0; i < 3; i++)
            {
                foreach (var article in clusters[i].Articles)
                {
                    DBNewsArticle dbArticle = new DBNewsArticle(article, i);
                    conn.Insert(dbArticle);
                }
            }
        }

        /**
         * Returns the three clusters of news articles in an array
         */
        public Cluster[] GetClusters()
        {
            Cluster[] clusters = new Cluster[3];
            for (int i = 0; i < 3; i++)
            {
                clusters[i] = new Cluster();
            }

            var articles = conn.Table<DBNewsArticle>();

            foreach (DBNewsArticle article in articles) 
            {
                clusters[article.ClusterNumber].Articles.Add(new NewsArticle(article));
            }

            return clusters;
        }

        /**
         * Returns a DateTime of the last time the clusters were updated
         */
        public DateTime LastUpdated()
        {
            return File.GetLastWriteTime(dbPath);
        }

        /**
         * Updates the list of sources in the database if 14 days have passed 
         * since the last update and returns the list of sources
         */
        public List<NewsSource> GetSources()
        {
            if ((DateTime.Now - SourcesLastUpdated()).TotalDays >= 14 || FirstTime)
            {
                sourcesConn.Query<NewsSource>("DELETE FROM Sources");

                var sources = ArticleFetcher.GetListOfSources();
                foreach (var source in sources)
                {
                    sourcesConn.Insert(source);
                }
                return sources;
            }
            else
            {
                var temp = sourcesConn.Table<NewsSource>();
                var sources = new List<NewsSource>();
                foreach (var source in temp)
                {
                    sources.Add(source);
                }
                return sources;
            }
        }

        private DateTime SourcesLastUpdated()
        {
            return File.GetLastWriteTime(sourcesDbPath);
        }
    }
}
