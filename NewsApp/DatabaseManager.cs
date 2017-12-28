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
        // TODO: Cache sources in separate database, update every week or two

        public DatabaseManager()
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Clusters");

            try
            {
                conn = new SQLiteConnection(dbPath);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddNewClusters(Cluster[] clusters)
        {
            // Delete old ones then add new ones
            conn.Query<DBNewsArticle>("DELETE FROM DBNewsArticle");

            for (int i = 0; i < 3; i++)
            {
                foreach (var article in clusters[i].Articles)
                {
                    DBNewsArticle dbArticle = new DBNewsArticle(article, i);
                    conn.Insert(dbArticle);
                }
            }
        }

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

        public DateTime LastUpdated()
        {
            return File.GetLastWriteTime(dbPath);
        }
    }
}
