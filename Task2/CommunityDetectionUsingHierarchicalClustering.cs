using MAD2_Tasks.General.Code;
using MAD2_Tasks.General.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class CommunityDetectionUsingHierarchicalClustering
    {
        public List<List<List<int>>> GetComunities(Dictionary<int, List<int>> network, out double[][] similarityMatrix)
        {
            var allComunityHierarchy = new List<List<List<int>>>();

            var adjacencyMatrix = network.ToAdjacencyMatrix();
            similarityMatrix = adjacencyMatrix.ToSimilarityMatrix();

            var primitiveComunities = new List<List<int>>();

            InitPrimitiveComunities(primitiveComunities, adjacencyMatrix);

            var lastProcessedComunities = primitiveComunities;

            while(lastProcessedComunities.Count > 1)
            {
                var newComunity = MergeComunities(lastProcessedComunities, similarityMatrix);

                allComunityHierarchy.Add(newComunity);

                lastProcessedComunities = newComunity;
            }


            //Predtim kvuli maticim pracujeme se snizenym ID
            foreach (var topLevelComunity in allComunityHierarchy)
            {
                foreach(var secondLevelComunity in topLevelComunity)
                {
                    for (int i = 0; i < secondLevelComunity.Count; i++)
                    {
                        secondLevelComunity[i] = secondLevelComunity[i] + 1;
                    }
                }
            }

            return allComunityHierarchy;
        }

        public Dictionary<int, Tuple<int, List<int>>> GetNetworkWithComunityId(Dictionary<int, List<int>> originalNetwork, List<List<int>> comunities)
        {
            var networkWithComunityId = new Dictionary<int, Tuple<int, List<int>>>();

            foreach(var node in originalNetwork)
            {
                var comunityId = 0;

                for (int i = 0; i < comunities.Count; i++)
                {
                    if(comunities[i].Contains(node.Key))
                    { 
                        comunityId = i;
                        break;
                    }
                }

                networkWithComunityId.Add(node.Key, new Tuple<int, List<int>>(comunityId, node.Value));
            }

            return networkWithComunityId;
        }

        private void InitPrimitiveComunities(List<List<int>> comunities, int[][] adjacencyMatrix)
        {
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                comunities.Add(new List<int>() { i });
            }
        }

        private List<List<int>> MergeComunities(List<List<int>> comunities, double[][] similarityMatrix)
        {
            var newComunityList = comunities.Clone();
            
            var comunitySimilarities = new List<Tuple<List<int>, List<int>, double>>();

            for (int i = 0; i < newComunityList.Count - 1; i++)
            {
                for (int y = i + 1; y < newComunityList.Count; y++)
                {
                    var similarity = SimilarityDistanceHelper.GetSingleLinkageSimilaritiy(newComunityList[i], newComunityList[y], similarityMatrix);

                    comunitySimilarities.Add(new Tuple<List<int>, List<int>, double>(newComunityList[i], newComunityList[y], similarity));
                }
            }

            var maxSimilarity = Enumerable.Max(comunitySimilarities.Select(x => x.Item3));


            var theMostSimilarComunities = comunitySimilarities.Where(x => x.Item3 == maxSimilarity).FirstOrDefault();


            var newComunity = new List<int>();

            var comunity1 = theMostSimilarComunities.Item1;
            var comunity2 = theMostSimilarComunities.Item2;

            newComunity.AddRange(comunity1);
            newComunity.AddRange(comunity2);

            newComunityList.Remove(comunity1);
            newComunityList.Remove(comunity2);
            newComunityList.Add(newComunity);

            return newComunityList;
        }
    }
}
