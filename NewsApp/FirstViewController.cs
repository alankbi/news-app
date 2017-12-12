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

            /*
            var a1 = new NewsArticle();
            a1.Title = "mueller probe shows new findings on trump jr";
            var a2 = new NewsArticle();
            a2.Title = "flynn pleads guilty to perjury after mueller probe indicts him";
            var a3 = new NewsArticle();
            a3.Title = "environmental studies have revealed a new study on health";
            DocumentClusterer d = new DocumentClusterer(new List<NewsArticle>(new NewsArticle[] { a1, a2, a3 }));*/
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
