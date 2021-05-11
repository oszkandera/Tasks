using System;
using System.Collections.Generic;
using System.Linq;

namespace MAD2_Tasks.General.Algorithms
{
    public class ModularityRankProcessor
    {
        public double CalculateModularity(int[][] network, string[] classes)
        {
            var totalNumberOfEdges = GetNumberOfEdges(network);
            var nodesDegree = GetNodesDegree(network);

            var tempSum = 0;
            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    var kroneckerDelta = classes[i] == classes[j] ? 1 : 0;
                    var Aij = network[i][j];
                    var kikj = nodesDegree[i] * nodesDegree[j];

                    tempSum += (Aij - (kikj / (2 * totalNumberOfEdges))) * kroneckerDelta;
                }
            }

            return (1 / (double)(2 * totalNumberOfEdges)) * tempSum;
        }

        public double Modularity(Dictionary<int, List<int>> graph, Dictionary<int, int> partition)
        {
            Dictionary<int, double> inc = new Dictionary<int, double>();
            Dictionary<int, double> deg = new Dictionary<int, double>();

            //double links = graph.Size;
            double links = Enumerable.Sum(graph.Values.Select(x => x.Count)) / 2;

            foreach (var node in graph)
            {
                int com = partition[node.Key];
                deg[com] = deg.ContainsKey(com) ? deg[com] + node.Value.Count : node.Value.Count;
                //deg[com] = DictGet(deg, com, 0) + graph.Degree(node);
                foreach (var neighbor in node.Value)
                {
                    if (partition[neighbor] == com)
                    {
                        double weight;
                        if (neighbor == node.Key)
                        {
                            weight = 1;
                        }
                        else
                        {
                            weight = 0 / 2;
                        }

                        inc[com] = inc.ContainsKey(com) ? inc[com] + weight : weight;// DictGet(inc, com, 0) + weight;
                    }
                }
            }

            double res = 0;
            foreach (int component in partition.Values.Distinct())
            {
                //res += DictGet(inc, component, 0) / links - Math.Pow(DictGet(deg, component, 0) / (2 * links), 2);
                res += (inc.ContainsKey(component) ? inc[component] : 0) / links - Math.Pow(deg[component] / (2 * links), 2);
            }
            return res;
        }

        private int GetNumberOfEdges(int[][] network)
        {
            var numberOfEdges = 0;
            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    numberOfEdges += network[i][j];
                }
            }

            return numberOfEdges / 2;
        }

        private int[] GetNodesDegree(int[][] network)
        {
            var nodesDegree = new int[network.Length];

            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    nodesDegree[i] += network[i][j];
                }
            }

            return nodesDegree;
        }
    }
}
