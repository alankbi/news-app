using System;
using System.Collections.Generic;

using UIKit;

namespace NewsApp
{
    public partial class FirstViewController : UIViewController
    {
        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        partial void DoTestClustering(UIButton sender)
        {
            // TODO: Do this on loading, check if its time to update, pass cached sources
            // to article fetcher, from then on backend should be close to finished ignoring
            // performance tweaks
            var fetcher = new ArticleFetcher();
            var articles = fetcher.GetArticles();

            DocumentClusterer d = new DocumentClusterer(articles);
            Cluster[] clusters = d.cluster(8);

            DatabaseManager m = new DatabaseManager();
            m.AddNewClusters(clusters);

            /* DEBUGGING CODE
            foreach (NewsArticle a in articles)
            {
                Console.WriteLine(a.Title + "\n" + a.Description);
            }
            
            var testClusters = m.GetClusters();
            Console.WriteLine(testClusters.Length + " CLUSTERS");
            int k = 0;
            foreach (Cluster c in testClusters)
            {
                foreach (NewsArticle article in c.Articles)
                {
                    k++;
                    Console.WriteLine("START OF ARTICLE");
                    Console.WriteLine(article.Title);
                    Console.WriteLine(article.Description);
                    Console.WriteLine(article.Url);
                    Console.WriteLine(article.UrlToImage);
                    Console.WriteLine(article.Source.ID);
                    Console.WriteLine(article.Source.Name);
                    Console.WriteLine("END OF ARTICLE");
                }
                Console.WriteLine("END OF CLUSTER");
                Console.WriteLine();
            }
            Console.WriteLine(k + " ARTICLES");

            var a1 = new NewsArticle();
            a1.Title = "he was for unique1 it and to of";
            var a2 = new NewsArticle();
            a2.Title = "the it and secondunique of to";
            var a3 = new NewsArticle();
            a3.Title = "with are on thethirdone in a to of";
            DocumentClusterer d = new DocumentClusterer(new List<NewsArticle>(new NewsArticle[] { a1, a2, a3 }));*/
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
