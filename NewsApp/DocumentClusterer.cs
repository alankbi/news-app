using System;
using System.Collections.Generic;


namespace NewsApp
{
    public class DocumentClusterer
    {
        List<NewsArticle> articles;
        DocumentAnalyzer analyzer;

        public DocumentClusterer(List<NewsArticle> articles)
        {
            this.articles = articles;
            analyzer = new DocumentAnalyzer(articles);
            DebugSimilarity();

        }

        public void cluster(int k)
        {
            // TODO: cluster articles using k-means based on tf-idf of documents. 
        }

        private void DebugSimilarity()
        {
            for (int i = 0; i < articles.Count - 1; i++)
            {
                double similarity = analyzer.similarity(i, i + 1);
                if (similarity >= 0)
                {
                    Console.WriteLine(articles[i].Title + " ### " + articles[i + 1].Title);
                    Console.WriteLine(similarity);
                }
            }
        }
    }
}
