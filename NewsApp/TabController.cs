using System;
using UIKit;
namespace NewsApp
{
    public class TabController : UITabBarController
    {
        ArticleViewController tab1, tab2, tab3;

        public TabController()
        {
            var manager = new DatabaseManager();
            Cluster[] clusters;

            double timePassed = (DateTime.Now - manager.LastUpdated()).TotalHours;
            Console.WriteLine("Hours since last update: " + timePassed);

            if (timePassed >= 8 || manager.FirstTime)
            {
                Console.WriteLine("Articles Updated");
                var fetcher = new ArticleFetcher(manager.GetSources());
                var articles = fetcher.GetArticles();

                var d = new DocumentClusterer(articles);
                clusters = d.cluster(20);
                manager.AddNewClusters(clusters);
            }
            else
            {
                clusters = manager.GetClusters();
            }

            DateTime needsUpdate = manager.LastUpdated().AddHours(8);

            tab1 = new ArticleViewController(clusters[0], needsUpdate);
            tab1.Title = "First";

            tab2 = new ArticleViewController(clusters[1], needsUpdate);
            tab2.Title = "Second";

            tab3 = new ArticleViewController(clusters[2], needsUpdate);
            tab3.Title = "Third";

            var tabs = new UIViewController[] {
                tab1, tab2, tab3
            };

            ViewControllers = tabs;
        }
    }
}
