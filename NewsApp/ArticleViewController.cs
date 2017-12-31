using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using Foundation;

using UIKit;

namespace NewsApp
{
    public partial class ArticleViewController : UIViewController
    {
        private List<NewsArticle> articles;
        private int index;

        private float Width = (float) UIScreen.MainScreen.Bounds.Width; // 375 iPhone 8
        private float Height = (float)UIScreen.MainScreen.Bounds.Height; // 667

        UINavigationBar bar;
        UITextView barText;

        UITextView articleTitle;
        UIImageView articleImage;

        public ArticleViewController(Cluster cluster) : base("ArticleViewController", null)
        {
            this.articles = cluster.Articles;
            index = 0;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            bar = new UINavigationBar(new CGRect(0, 0, Width, 45));
            bar.BarTintColor = UIColor.Blue;
            View.AddSubview(bar);

            barText = new UITextView(new RectangleF(0, 8, Width, 45));
            barText.Text = "NewsApp";
            barText.TextAlignment = UITextAlignment.Center;
            barText.Font = UIFont.SystemFontOfSize(18);
            barText.TextColor = UIColor.White;
            barText.BackgroundColor = UIColor.Clear;
            barText.Editable = false;
            barText.Selectable = false;
            View.AddSubview(barText);

            articleImage = new UIImageView(new RectangleF(Width / 20, (float) bar.Frame.Bottom + Height / 20, Width - Width / 10, Height / 4));
            articleImage.Image = FromUrl(articles[index].UrlToImage);Console.WriteLine(articles[index].UrlToImage);
            View.AddSubview(articleImage);

            // Insert here source name and article url

            articleTitle = new UITextView(new RectangleF(Width / 20, (float) articleImage.Frame.Bottom + Height / 10, Width - Width / 10, Height / 4));
            articleTitle.Text = articles[index].Title;
            articleTitle.Font = UIFont.SystemFontOfSize(14 + (int) (Width / 30));
            articleTitle.TextColor = UIColor.Black;
            //articleTitle.BackgroundColor = UIColor.Blue; // DEBUG
            articleTitle.Editable = false;
            articleTitle.Selectable = false;
            View.AddSubview(articleTitle);


        }

        private UIImage FromUrl(string uri)
        {
            if (uri == null) return null;
            using (var url = new NSUrl(uri))
            {
                using (var data = NSData.FromUrl(url))
                {
                    if (data == null)
                    {
                        return null;
                    }
                    return UIImage.LoadFromData(data);
                }
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}

