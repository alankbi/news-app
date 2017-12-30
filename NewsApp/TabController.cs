using System;
using UIKit;
namespace NewsApp
{
    public class TabController : UITabBarController
    {
        ArticleViewController tab1, tab2, tab3;

        public TabController()
        {
            tab1 = new ArticleViewController();
            tab1.Title = "First";
            //tab1.SetTitle("TESTING...");

            tab2 = new ArticleViewController();
            tab2.Title = "Second";

            tab3 = new ArticleViewController();
            tab3.Title = "Third";

            var tabs = new UIViewController[] {
                tab1, tab2, tab3
            };

            ViewControllers = tabs;
        }
    }
}
