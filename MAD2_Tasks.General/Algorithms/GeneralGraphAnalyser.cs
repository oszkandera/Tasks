using MAD2_Tasks.General.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MAD2_Tasks.General.Algorithms
{
    public class GeneralGraphAnalyser
    {
        public int GetNumberOfNodes(Dictionary<int, List<int>> graph)
        {
            return graph.Count;
        }

        public int GetNumberOfEdges(Dictionary<int, List<int>> graph, bool isOriented = false)
        {
            var sum = Enumerable.Sum(graph.Select(x => x.Value.Count));
            
            if (!isOriented)
            {
                sum = sum / 2;
            }

            return sum;
        }

        public int GetMaxDegree(Dictionary<int, List<int>> graph)
        {
            return Enumerable.Max(graph.Select(x => x.Value.Count));
        }

        public double GetAverageDegree(Dictionary<int, List<int>> graph)
        {
            return Enumerable.Average(graph.Select(x => x.Value.Count));
        }

        public List<HashSet<int>> GetComponents(Dictionary<int, List<int>> graph)
        {
            var components = new List<HashSet<int>>();

            var nodeStatus = new Dictionary<int, BFSStatus>();
            foreach(var node in graph)
            {
                nodeStatus.Add(node.Key, BFSStatus.NoVisited);
            }

            var queue = new Queue<int>();
            queue.Enqueue(graph.FirstOrDefault().Key);
            var visitedNodeCount = 0;

            var component = new HashSet<int>();

            do
            {
                var initNode = queue.Dequeue();
                nodeStatus[initNode] = BFSStatus.Visited;
                visitedNodeCount++;

                component.Add(initNode);

                var neighbors = graph[initNode];

                foreach (var neighbor in neighbors)
                {
                    if (nodeStatus[neighbor] == BFSStatus.NoVisited)
                    {
                        queue.Enqueue(neighbor);
                    }
                }

                if(queue.Count == 0)
                {
                    components.Add(component);
                    component = new HashSet<int>();
                    var noVisitedNodes = nodeStatus.Where(x => x.Value == BFSStatus.NoVisited);

                    if (noVisitedNodes.Any())
                    {
                        var newInitNode = noVisitedNodes.First();
                        queue.Enqueue(newInitNode.Key);
                    }
                }
            }
            while (queue.Count > 0);

            return components;
        }

        public double GetAverageClusteringCoeficient(Dictionary<int, List<int>> graph)
        {
            var clusteringCoeficients = GetClusteringCoeficients(graph);
            return Enumerable.Sum(clusteringCoeficients.Select(x => x.Item2)) / clusteringCoeficients.Count;
        }

        public List<Tuple<int, double>> GetClusteringCoeficients(Dictionary<int, List<int>> graph)
        {
            var clusteringCoeficients = new List<Tuple<int, double>>();
            foreach (var node in graph)
            {
                var nodeDegree = node.Value.Count;
                var numberOfConnectionsBetweenNeighboards = GetNumberOfConnectionsBetweenNeighboards(node, graph);
                var clusteringCoeficient = nodeDegree <= 1 ? 0.0 : (double)(numberOfConnectionsBetweenNeighboards) / (nodeDegree * (nodeDegree - 1));
                clusteringCoeficients.Add(new Tuple<int, double>(node.Key, clusteringCoeficient));
            }

            return clusteringCoeficients;
        }

        private static int GetNumberOfConnectionsBetweenNeighboards(KeyValuePair<int, List<int>> node, Dictionary<int, List<int>> data)
        {
            var numberOfConnectionsBetweenNeighboards = 0;
            foreach (var neighboardId in node.Value)
            {
                var neigboardsOfNeighboard = data[neighboardId];

                foreach (var neighboardOfNeighboard in neigboardsOfNeighboard)
                {
                    if (neighboardOfNeighboard == node.Key) continue;

                    if (node.Value.Contains(neighboardOfNeighboard)) numberOfConnectionsBetweenNeighboards++;
                }
            }

            return numberOfConnectionsBetweenNeighboards;
        }
    }
}
