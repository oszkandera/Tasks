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
    }
}
