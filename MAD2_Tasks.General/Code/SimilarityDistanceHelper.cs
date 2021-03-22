using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAD2_Tasks.General.Code
{
    public static class SimilarityDistanceHelper
    {
        public static double GetSingleLinkageSimilaritiy(IEnumerable<int> cluster1, IEnumerable<int> cluster2, double[][] similarityMatrix)
        {
            var minSimilarity = default(double?);

            foreach(var clusterItem1 in cluster1)
            {
                foreach(var clusterItem2 in cluster2)
                {
                    var similarity = similarityMatrix[clusterItem1][clusterItem2];

                    if(!minSimilarity.HasValue || similarity > minSimilarity)
                    {
                        minSimilarity = similarity;
                    }
                }
            }
            return minSimilarity.Value;
        }

        public static double GetCompleteLinkageSimilarity(IEnumerable<int> cluster1, IEnumerable<int> cluster2, double[][] similarityMatrix)
        {
            var maxSimilarity = default(double?);

            foreach (var clusterItem1 in cluster1)
            {
                foreach (var clusterItem2 in cluster2)
                {
                    var similarity = similarityMatrix[clusterItem1][clusterItem2];

                    if (!maxSimilarity.HasValue || similarity > maxSimilarity)
                    {
                        maxSimilarity = similarity;
                    }
                }
            }
            return maxSimilarity.Value;
        }
    }
}
