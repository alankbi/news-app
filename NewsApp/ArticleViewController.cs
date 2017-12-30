using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;

using UIKit;

namespace NewsApp
{
    public partial class ArticleViewController : UIViewController
    {
        private List<NewsArticle> articles;
        private int index;

        private float Width = (float) UIScreen.MainScreen.Bounds.Width;
        private float Height = (float)UIScreen.MainScreen.Bounds.Height;


        public ArticleViewController(Cluster cluster) : base("ArticleViewController", null)
        {
            this.articles = cluster.Articles;
            index = 0;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var bar = new UINavigationBar(new CGRect(0, 0, Width, 50));
            //View.AddSubview(bar);

            articleTitle.Text = articles[index].Title;
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}

