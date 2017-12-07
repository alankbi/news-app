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
            Console.WriteLine("Test");

            var fetcher = new ArticleFetcher();
            var articles = fetcher.GetArticles();

            foreach (NewsArticle a in articles)
            {
                Console.WriteLine(a.Title + "\n" + a.Description);
            }

            DocumentClusterer d = new DocumentClusterer(articles);
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
