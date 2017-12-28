using System;
using System.Collections.Generic;

namespace NewsApp
{
    public class Cluster
    {
        public double[] Centroid { get; set; }
        // Document maps to indexes for clustering, afterwards mapped to articles
        public List<int> Documents { get; set; }
        public List<NewsArticle> Articles { get; set; }

        public Cluster()
        {
            Documents = new List<int>();
        }

        public int CompareTo(Cluster other, DocumentAnalyzer analyzer)
        {
            // Best clusters should have lower number (-1)

            double simA, simB;
            simA = simB = 0;

            foreach (int i in this.Documents)
            {
                simA += analyzer.Similarity(this.Centroid, i);
            }
            foreach (int i in other.Documents)
            {
                simB += analyzer.Similarity(other.Centroid, i);
            }
            simA /= this.Documents.Count;
            simB /= other.Documents.Count;

            if (simA > simB)
            {
                return -1;
            }
            else if (simA < simB)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }
    }
}
