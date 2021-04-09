using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public class CopyingModelGenerator
    {
        public Dictionary<int, List<int>> Generate(int size, double p, double internalLinkGenerationProbability = 0.0)
        {
            var graph = new Dictionary<int, List<int>>();
            var random = new Random();

            graph.Add(0, new List<int>());

            for (int i = 1; i < size; i++)
            {
                var newNode = i;
                var numberOfNodesInGraph = i - 1;
                var randomlySelectedNode = random.Next(0, numberOfNodesInGraph);

                ProcessAddAction(graph, random, p, newNode, randomlySelectedNode);

                var shouldGenerateLinkBetweenPreExistingNodes = random.NextDouble() < internalLinkGenerationProbability;
                if (shouldGenerateLinkBetweenPreExistingNodes)
                {
                    ProcessInternalLinkGeneration(graph, random, p);
                }
            }

            return graph;
        }

        private void ProcessInternalLinkGeneration(Dictionary<int, List<int>> graph, Random random, double p)
        {
            var isSuccesfullyAdded = false;
            while (!isSuccesfullyAdded)
            {
                var from = random.Next(graph.Count);
                var to = random.Next(graph.Count);
                
                isSuccesfullyAdded = ProcessAddAction(graph, random, p, from, to);
            }
        }

        private bool ProcessAddAction(Dictionary<int, List<int>> graph, Random random, double p, int from, int to)
        {
            var shouldChooseNeighbor = random.NextDouble() > p;
            if (shouldChooseNeighbor && graph[to].Any())
            {
                var randomlySelectedNeighborIndex = random.Next(graph[to].Count);
                var randomlySelectedNeighbor = graph[to][randomlySelectedNeighborIndex];
                
                return AddUniDirectionLink(graph, from, randomlySelectedNeighbor);
            }

             return AddUniDirectionLink(graph, from, to);
        }

        private bool AddUniDirectionLink(Dictionary<int, List<int>> graph, int from, int to)
        {
            if (!graph.ContainsKey(from)) graph.Add(from, new List<int>());
            if (!graph.ContainsKey(to)) graph.Add(to, new List<int>());

            if(ExistsEdge(graph, from, to) || IsLoop(from, to))
            {
                return false;
            }

            graph[from].Add(to);
            return true;
        }

        private bool ExistsEdge(Dictionary<int, List<int>> graph, int from, int to)
        {
            return graph.ContainsKey(from) && graph[from].Contains(to);
        }

        private bool IsLoop(int from, int to)
        {
            return from == to;
        }
    }
}
