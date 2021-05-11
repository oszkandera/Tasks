using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MAD2_Tasks.General.Code
{
    public class AdjacencyListGraphLoader
    {
        public Dictionary<int, List<int>> Load(string path, string delimiter = ";")
        {
            var graph = new Dictionary<int, List<int>>();
            var rawData = File.ReadAllLines(path);

            foreach (var row in rawData)
            {
                var nodesInRow = row.Split(delimiter);
                var x = Int32.Parse(nodesInRow[0]);
                var y = Int32.Parse(nodesInRow[1]);

                AddBidirectNode(graph, x, y);
            }

            return graph.OrderBy(x => x.Key).ToDictionary(k => k.Key, v => v.Value);
        }

        private void AddBidirectNode(Dictionary<int, List<int>> graph, int node1, int node2)
        {
            if (!graph.ContainsKey(node1)) { graph[node1] = new List<int>(); }
            if (!graph.ContainsKey(node2)) { graph[node2] = new List<int>(); }

            if (!graph[node1].Contains(node2)) { graph[node1].Add(node2); }
            if (!graph[node2].Contains(node1)) { graph[node2].Add(node1); }
        }
    }
}
