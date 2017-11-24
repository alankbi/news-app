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

            ArticleFetcher fetcher = new ArticleFetcher();
            List<NewsArticle> articles = fetcher.GetArticles();

			foreach (NewsArticle article in articles)
			{
				Console.WriteLine(article.title);
				Console.WriteLine(article.source.name);
			}
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
