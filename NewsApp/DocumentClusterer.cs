using System;
using System.Collections.Generic;
using System.Linq;


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
            // 1. initialize random cluster points, preferably like < 0.5 similarity
            // 2. Assign docs to closest cluster (cos sim, centroid is avg tf idf)
            // 3. Change cluster centroids to new centroid
            // 4. If centroids dont change, stop program, then do sorting task

            var clusters = new Cluster[k];
            var r = new Random();
            // Initialize k clusters by choosing random documents as starting points
            for (int i = 0; i < k; i++)
            {
                clusters[i] = new Cluster();
                bool run = true;
                int count1 = 0;
                while (run && count1 < 10)
                {
                    count1++;
                    // Choose random document to initialize cluster to
                    int index = (int)(r.NextDouble() * articles.Count);
                    clusters[i].Centroid = analyzer.GetDocumentTfIdf(index);
                    bool continueRunning = false;
                    for (int j = 0; j < i; j++)
                    {
                        // Keep running if this cluster is too similar to an already chosen cluster
                        if (analyzer.Similarity(clusters[j].Centroid, clusters[i].Centroid) >= 0.1)
                        {
                            continueRunning = true;
                            break;
                        }
                    }
                    run = continueRunning;
                }
            }

            int count2 = 0;
            bool stillChanging = true;
            while (stillChanging && count2 < 20)
            {
                count2++;
                foreach (Cluster c in clusters)
                {
                    c.Documents.Clear();
                }

                // Assign each article the cluster it's closest to
                for (int i = 0; i < articles.Count; i++)
                {
                    double greatestSim = 0;
                    int clusterIndex = 0;
                    for (int j = 0; j < clusters.Length; j++)
                    {
                        double currentSim = analyzer.Similarity(clusters[j].Centroid, i);
                        if (currentSim > greatestSim)
                        {
                            greatestSim = currentSim;
                            clusterIndex = j;
                        }
                    }
                    clusters[clusterIndex].Documents.Add(i);
                }

                // Update cluster centroids
                var newCentroids = new double[clusters.Length][];
                for (int i = 0; i < clusters.Length; i++)
                {
                    var newCentroid = new double[analyzer.NumberOfTerms()];
                    var currDocs = clusters[i].Documents;
                    for (int j = 0; j < currDocs.Count; j++)
                    {
                        var values = analyzer.GetDocumentTfIdf(currDocs[j]);
                        for (int l = 0; l < newCentroid.Length; l++)
                        {
                            newCentroid[l] += values[l];
                        }
                    }
                    for (int j = 0; j < newCentroid.Length; j++)
                    {
                        newCentroid[j] /= currDocs.Count;
                    }
                    newCentroids[i] = newCentroid;
                }

                // Finish if clusters remain unchanged
                bool unchanged = true;
                for (int i = 0; i < clusters.Length; i++)
                {
                    if (!newCentroids[i].SequenceEqual(clusters[i].Centroid))
                    {
                        unchanged = false;
                        clusters[i].Centroid = newCentroids[i];
                    }
                }
                stillChanging = !unchanged;
            }

            foreach (Cluster c in clusters)
            {
                foreach (int index in c.Documents)
                {
                    Console.WriteLine(articles[index].Title);
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine(count2 + " loops");

        }

        private void DebugSimilarity()
        {
            for (int i = 0; i < articles.Count - 1; i++)
            {
                double similarity = analyzer.Similarity(i, i + 1);
                if (similarity >= 0)
                {
                    Console.WriteLine(articles[i].Title + " ### " + articles[i + 1].Title);
                    Console.WriteLine(similarity);
                }
            }
        }
    }
}
