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

        private UISwipeGestureRecognizer gestureLeft;
        private UISwipeGestureRecognizer gestureRight;

        private UIView[] articleDisplays;

        private float Width = (float) UIScreen.MainScreen.Bounds.Width; // 375 iPhone 8
        private float Height = (float)UIScreen.MainScreen.Bounds.Height; // 667

        UINavigationBar bar;
        UILabel barText;
        UILabel similarArticles;

        UIButton leftButton;
        UIButton rightButton;

        public ArticleViewController(Cluster cluster) : base("ArticleViewController", null)
        {
            this.articles = cluster.Articles;
            index = 0;

            articleDisplays = new UIView[articles.Count];

            gestureLeft = new UISwipeGestureRecognizer();
            gestureLeft.Direction = UISwipeGestureRecognizerDirection.Left;
            gestureLeft.AddTarget(() => HandleSwipe(index + 1));
            View.AddGestureRecognizer(gestureLeft);

            gestureRight = new UISwipeGestureRecognizer();
            gestureRight.Direction = UISwipeGestureRecognizerDirection.Right;
            gestureRight.AddTarget(() => HandleSwipe(index - 1));
            View.AddGestureRecognizer(gestureRight);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            bar = new UINavigationBar(new CGRect(0, 0, Width, 45));
            bar.BarTintColor = UIColor.Blue;
            View.AddSubview(bar);

            barText = new UILabel(new RectangleF(0, 8, Width, 45));
            barText.Text = "NewsApp";
            barText.TextAlignment = UITextAlignment.Center;
            barText.Font = UIFont.SystemFontOfSize(18);
            barText.TextColor = UIColor.White;
            barText.BackgroundColor = UIColor.Clear;
            View.AddSubview(barText);

            similarArticles = new UILabel(new RectangleF(0, Height - Height / 6, Width, Height * 2 / 15));
            similarArticles.Lines = 0;
            similarArticles.Text = "Similar Articles (1/" + articles.Count + ")";
            similarArticles.TextAlignment = UITextAlignment.Center;
            similarArticles.Font = UIFont.SystemFontOfSize(14 + (int)(Width / 25));
            similarArticles.AdjustsFontForContentSizeCategory = true;
            similarArticles.TranslatesAutoresizingMaskIntoConstraints = true;
            similarArticles.SizeToFit();
            similarArticles.Frame = new RectangleF((float)(Width / 2 - similarArticles.Frame.Width / 2 - 6), Height - Height / 6, (float)(similarArticles.Frame.Width + 12), (float)similarArticles.Frame.Height);
            similarArticles.TextColor = UIColor.Black;
            similarArticles.BackgroundColor = UIColor.Gray; // DEBUG
            View.AddSubview(similarArticles);

            leftButton = UIButton.FromType(UIButtonType.System);
            leftButton.Frame = new RectangleF(0, (float)similarArticles.Frame.Top, (float)similarArticles.Frame.Left - 5, (float)similarArticles.Frame.Height);
            leftButton.SetTitle("<", UIControlState.Normal);
            leftButton.Font = UIFont.SystemFontOfSize(14 + (int)(Width / 25));
            leftButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
            leftButton.Enabled = false;
            View.AddSubview(leftButton);

            rightButton = UIButton.FromType(UIButtonType.System);
            rightButton.Frame = new RectangleF((float)similarArticles.Frame.Right + 5, (float)similarArticles.Frame.Top, (float)(similarArticles.Frame.Left - 5), (float)similarArticles.Frame.Height);
            rightButton.SetTitle(">", UIControlState.Normal);
            rightButton.Font = UIFont.SystemFontOfSize(14 + (int)(Width / 25));
            rightButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            View.AddSubview(rightButton);

            leftButton.TouchUpInside += (sender, e) => 
            {
                HandleSwipe(index - 1);
            };

            rightButton.TouchUpInside += (sender, e) => 
            {
                HandleSwipe(index + 1);
            };

            for (int i = 0; i < articleDisplays.Length; i++)
            {
                articleDisplays[i] = InitializeArticleDisplays(articles[i]);
            }

            View.AddSubview(articleDisplays[index]);
        }

        private UIView InitializeArticleDisplays(NewsArticle article)
        {
            var tempView = new UIView();

            UILabel articleTitle;
            UIImageView articleImage;
            UILabel articleUrl;
            UILabel articleSource;
            UITextView articleDescription; // UILabels don't align to the top

            articleImage = new UIImageView(new RectangleF(Width / 20, (float)bar.Frame.Bottom + Height / 10, Width - Width / 10, Height / 3));
            articleImage.Image = FromUrl(article.UrlToImage);
            tempView.AddSubview(articleImage);

            articleSource = new UILabel(new RectangleF(Width / 20, (float)articleImage.Frame.Top - 70, Width - Width / 10, 70));
            articleSource.Text = article.Source.Name;
            articleSource.Font = UIFont.SystemFontOfSize(15 + (int)(Width / 20));
            articleSource.AdjustsFontSizeToFitWidth = true;
            articleSource.AdjustsFontForContentSizeCategory = true;
            articleSource.TranslatesAutoresizingMaskIntoConstraints = true;
            articleSource.SizeToFit();
            articleSource.Frame = new RectangleF(Width / 20, (float)(articleImage.Frame.Top - articleSource.Frame.Height), Width - Width / 10, (float)articleSource.Frame.Height);
            articleSource.TextColor = UIColor.Black;
            articleSource.BackgroundColor = UIColor.Blue; // DEBUG
            tempView.AddSubview(articleSource);

            articleUrl = new UILabel(new RectangleF(Width / 20, (float)articleImage.Frame.Bottom, Width - Width / 10, Height / 10));
            articleUrl.Lines = 1;
            articleUrl.Text = article.Url;
            articleUrl.Font = UIFont.SystemFontOfSize(6 + (int)(Width / 50));
            articleUrl.TranslatesAutoresizingMaskIntoConstraints = true;
            articleUrl.SizeToFit();
            articleUrl.Frame = new RectangleF(Width / 20, (float)articleImage.Frame.Bottom, Width - Width / 10, (float)articleUrl.Frame.Height);
            articleUrl.TextColor = UIColor.Gray;
            articleUrl.BackgroundColor = UIColor.Blue; // DEBUG
            tempView.AddSubview(articleUrl);

            articleTitle = new UILabel(new RectangleF(Width / 20, (float)articleUrl.Frame.Bottom + Height / 30, Width - Width / 10, Height / 4));
            articleTitle.Lines = 0;
            articleTitle.Text = article.Title;
            articleTitle.Font = UIFont.SystemFontOfSize(14 + (int)(Width / 30));
            articleTitle.TranslatesAutoresizingMaskIntoConstraints = true;
            articleTitle.SizeToFit();
            articleTitle.Frame = new RectangleF(Width / 20, (float)articleUrl.Frame.Bottom + Height / 30, Width - Width / 10, (float)articleTitle.Frame.Height);
            articleTitle.TextColor = UIColor.Black;
            articleTitle.BackgroundColor = UIColor.Blue; // DEBUG
            tempView.AddSubview(articleTitle);


            articleDescription = new UITextView(new RectangleF(Width / 20, (float)articleTitle.Frame.Bottom, Width - Width / 10, (float)(Height * 4 / 5 - articleTitle.Frame.Bottom)));
            articleDescription.Text = article.Description;
            articleDescription.Font = UIFont.SystemFontOfSize(10 + (int)(Width / 50));
            articleDescription.TextColor = UIColor.Black;
            articleDescription.BackgroundColor = UIColor.Gray; // DEBUG
            articleDescription.Editable = false;
            articleDescription.Selectable = false;
            tempView.AddSubview(articleDescription);

            return tempView;
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

        private void HandleSwipe(int newIndex)
        {
            // currently only recognizes right swipe
            if (newIndex >= articles.Count || newIndex < 0)
            {
                return;
            }

            UIView.Transition(articleDisplays[index], articleDisplays[newIndex], .3f, UIViewAnimationOptions.TransitionFlipFromTop, null);
            index = newIndex;

            similarArticles.Text = "Similar Articles (" + (index + 1) + "/" + articles.Count + ")";
            similarArticles.SetNeedsDisplay();

            leftButton.Enabled = true;
            rightButton.Enabled = true;

            if (index == articles.Count - 1)
            {
                rightButton.Enabled = false;
            } 
            else if (index == 0)
            {
                leftButton.Enabled = false;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}

