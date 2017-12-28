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
            var fetcher = new ArticleFetcher();
            var articles = fetcher.GetArticles();

            foreach (NewsArticle a in articles)
            {
                Console.WriteLine(a.Title + "\n" + a.Description);
            }
            DocumentClusterer d = new DocumentClusterer(articles);
            d.cluster(8);

            DatabaseManager m = new DatabaseManager();


            /*var a1 = new NewsArticle();
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
