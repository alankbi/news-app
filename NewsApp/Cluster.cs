using System;
using System.Collections.Generic;
namespace NewsApp
{
    public class Cluster
    {
        public double[] Centroid { get; set; }
        public List<int> Documents { get; set; }

        public Cluster()
        {
            Documents = new List<int>();
        }
    }
}
