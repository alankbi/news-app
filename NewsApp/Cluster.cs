using System;
using System.Collections.Generic;

namespace NewsApp
{
    public class Cluster
    {
        public double[] Centroid { get; set; }

        // Documents maps to indexes during clustering, afterwards mapped to articles
        public List<int> Documents { get; set; }
        public List<NewsArticle> Articles { get; set; }

        public Cluster()
        {
            Documents = new List<int>();
            Articles = new List<NewsArticle>();
        }

        /**
         * Used to compare between clusters. If this cluster is better, meaning less
         * deviation, returns a negative number so that it is put first when sorted.
         */
        public int CompareTo(Cluster other, DocumentAnalyzer analyzer)
        {
            // TODO: Find better method, this one favors smaller clusters
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
