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
        }

        public void cluster(int k)
        {
            // TODO: cluster articles using k-means based on tf-idf of documents. 
        }
    }
}
