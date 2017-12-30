using System;
using System.Drawing;

using UIKit;

namespace NewsApp
{
    public partial class ArticleViewController : UIViewController
    {
        public ArticleViewController() : base("ArticleViewController", null)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            articleTitle.Text = "TEISTO";
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}

