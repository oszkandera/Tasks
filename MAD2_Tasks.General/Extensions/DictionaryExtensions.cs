using MAD2_Tasks.General.Factory;
using System.Collections.Generic;
using System.Linq;

namespace MAD2_Tasks.General.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Initialize(this Dictionary<int, List<int>> source, int size)
        {
            for (int i = 0; i < size; i++)
            {
                source.Add(i, new List<int>());
            }
        }

        public static int[][] ToAdjacencyMatrix(this Dictionary<int, List<int>> source)
        {
            var allNodes = source.Select(x => x.Key).Union(source.SelectMany(x => x.Value)).Distinct();

            var isNumberedFromZero = allNodes.Contains(0);
            var matrixSize = isNumberedFromZero ? Enumerable.Max(allNodes) + 1 : Enumerable.Max(allNodes);

            var adjacencyMatrix = ArrayFactory.CreateMatrix(matrixSize, 0);

            foreach(var node in source)
            {
                foreach(var neighbor in node.Value)
                {
                    var mainNode = isNumberedFromZero ? node.Key : node.Key - 1;
                    var neighborNode = isNumberedFromZero ? neighbor : neighbor - 1;

                    adjacencyMatrix[mainNode][neighborNode] = 1;
                }
            }

            return adjacencyMatrix;
        }

        public static void RemoveParalelEdges(this Dictionary<int, List<int>> network)
        {
            foreach (var node in network)
            {
                for (int i = 0; i < node.Value.Count; i++)
                {
                    var neighbor = node.Value[i];
                    network[neighbor].Remove(node.Key);
                }
            }
        }

        public static void AddBidirectEdge(this Dictionary<int, List<int>> graph, int node1, int node2)
        {
            if (!graph.ContainsKey(node1)) graph.Add(node1, new List<int>());
            if (!graph.ContainsKey(node2)) graph.Add(node2, new List<int>());

            if (!graph[node1].Contains(node2)) graph[node1].Add(node2);
            if (!graph[node2].Contains(node1)) graph[node2].Add(node1);


            //graph[node1].Add(node2);
            //graph[node2].Add(node1);
        }
    }
}
