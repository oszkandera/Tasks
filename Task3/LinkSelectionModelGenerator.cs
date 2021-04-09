using System;
using System.Collections.Generic;

namespace Task3
{
    public class LinkSelectionModelGenerator
    {
        public Dictionary<int, List<int>> Generate(int size, double p, double internalLinkGenerationProbability = 0.0)
        {
            var links = new List<int[]>();

            var graph = new Dictionary<int, List<int>>();

            AddBidirectionLink(graph, 0, 1);    
            AddLink(links, 0, 1);

            var random = new Random();

            for (int i = 2; i < size; i++)
            {
                var from = i;          
                var randomLink = links[random.Next(links.Count)];
               
                ProcessAddAction(graph, links, random, p, from, randomLink);

                var shouldGenerateLinkBetweenPreExistingNodes = random.NextDouble() < internalLinkGenerationProbability;
                if (shouldGenerateLinkBetweenPreExistingNodes)
                {
                    ProcessInternalLinkGeneration(graph, links, random, p);
                }
            }

            return graph;
        }

        private void ProcessInternalLinkGeneration(Dictionary<int, List<int>> graph, List<int[]> links, Random random, double p)
        {
            var isSuccesfullyAdded = false;
            while (!isSuccesfullyAdded)
            {
                var from = random.Next(graph.Count);
                var randomLink = links[random.Next(links.Count)];

                isSuccesfullyAdded = ProcessAddAction(graph, links, random, p, from, randomLink);
            }
        }

        private bool ProcessAddAction(Dictionary<int, List<int>> graph, List<int[]> links, Random random, double p, int from, int[] link)
        {
            var shouldChooseFirstNode = random.NextDouble() > p;
            int to = shouldChooseFirstNode ? link[0] : link[1];

            var isAdded = AddBidirectionLink(graph, from, to);
            if (isAdded)
            {
                AddLink(links, from, to);
            }

            return isAdded;
        }

        private bool AddBidirectionLink(Dictionary<int, List<int>> graph, int from, int to)
        {
            if (!graph.ContainsKey(from)) graph.Add(from, new List<int>());
            if (!graph.ContainsKey(to))   graph.Add(to, new List<int>());

            if (ExistsEdge(graph, from, to) || IsLoop(from, to))
            {
                return false;
            }

            graph[from].Add(to);
            graph[to].Add(from);

            return true;
        }

        private void AddLink(List<int[]> links, int node1, int node2)
        {
            links.Add(new int[] { node1, node2 });
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
