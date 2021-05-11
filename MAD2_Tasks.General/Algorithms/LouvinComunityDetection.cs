using MAD2_Tasks.General.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD2_Tasks.General.Algorithms
{
    public class LouvinComunityDetection
    {
        private readonly ModularityRankProcessor _modularityRankProcessor;

        public LouvinComunityDetection(ModularityRankProcessor modularityRankProcessor)
        {
            _modularityRankProcessor = modularityRankProcessor;
        }

        public string DetectCommunities(Dictionary<int, List<int>> network)
        {
            var communities = PutEachNodeIntoSeparateComunity(network);
            var networkMatrix = network.ToAdjacencyMatrix();

            var numberOfNodes = network.Count;

            var communitiesDict = new Dictionary<int, int>();
            foreach(var node in network)
            {
                communitiesDict.Add(node.Key, node.Key);
            }

            communities[5] = "8";
            communitiesDict[5] = 8;

            var c1 = _modularityRankProcessor.CalculateModularity(networkMatrix, communities);
            var c2 = _modularityRankProcessor.Modularity(network, communitiesDict);
            for (int i = 0; i < numberOfNodes; i++)
            {
                var nodeId = i;
                var neighbors = GetNeighbors(networkMatrix, nodeId);
                var originalNodeComunity = communities[nodeId];

                var bestModularity = 0.0;
                var bestCommunityExchange = "";

                for (int y = 0; y < neighbors.Count; y++)
                {
                    var neighbordId = neighbors[y];
                    var neighborComunity = communities[neighbordId];
                    communities[nodeId] = neighborComunity;

                    //var modularity = _modularityRankProcessor.CalculateModularity(networkMatrix, communities);
                    var modularity = 10;
                    if(bestModularity < modularity)
                    {
                        bestModularity = modularity;
                        bestCommunityExchange = neighborComunity;
                    }
                }

                if(!String.IsNullOrWhiteSpace(bestCommunityExchange))
                {
                    communities[nodeId] = bestCommunityExchange;
                }
                else
                {
                    communities[nodeId] = originalNodeComunity;
                }
            }

            return "";
        }

        private List<int> GetNeighbors(int[][] matrix, int node)
        {
            var neighbors = new List<int>();

            for (int i = 0; i < matrix.Length; i++)
            {
                if(matrix[node][i] == 1)
                {
                    neighbors.Add(i);
                }
            }

            return neighbors;
        }

        private string[] PutEachNodeIntoSeparateComunity(Dictionary<int, List<int>> graph)
        {
            var communities = new string[graph.Count];

            for(int i = 0; i < graph.Count; i++)
            {
                communities[i] = i.ToString();
            }

            return communities;
        }
    }
}
