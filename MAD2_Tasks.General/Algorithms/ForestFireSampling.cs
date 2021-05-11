using MAD2_Tasks.General.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MAD2_Tasks.General.Algorithms
{
    public class ForestFireSampling
    {
        private readonly double _burnProbability;

        public ForestFireSampling() : this(0.7) { }

        public ForestFireSampling(double burnProbability)
        {
            _burnProbability = burnProbability;
        }

        public Dictionary<int, List<int>> GenerateSample(Dictionary<int, List<int>> graph, int size)
        {
            var numberOfNodes = graph.Count;
            if (numberOfNodes < size)
            {
                throw new ArgumentException("Size cannot be greater than then number of nodes in graph");
            }

            var sampleGraph = new Dictionary<int, List<int>>();
            var queue = new Queue<int>();
            var randomGenerator = new Random();

            var notVisitedNodes = new HashSet<int>();
            foreach(var node in graph)
            {
                notVisitedNodes.Add(node.Key);
            }

            AddRandomSeedToQueue(queue, randomGenerator, notVisitedNodes);

            while (sampleGraph.Count < size)
            {
                if (queue.Count <= 0)
                {
                    AddRandomSeedToQueue(queue, randomGenerator, notVisitedNodes);
                    continue;
                }

                var initialNodeId = queue.Dequeue();

                if (!notVisitedNodes.Contains(initialNodeId)) continue;
                notVisitedNodes.Remove(initialNodeId);

                if(!sampleGraph.ContainsKey(initialNodeId)) sampleGraph.Add(initialNodeId, new List<int>());

                var neighbors = graph[initialNodeId];

                foreach (var neighbor in neighbors)
                {
                    if (sampleGraph.Count >= size) break; //Zabrani preteceni pozadovaneho poctu vrcholu

                    var shouldBurnNeighbor = randomGenerator.NextDouble() <= _burnProbability;
                    if (shouldBurnNeighbor)
                    {
                        queue.Enqueue(neighbor);
                        sampleGraph.AddBidirectEdge(initialNodeId, neighbor);
                    }
                }
            }

            return sampleGraph;
        }

        private void AddRandomSeedToQueue(Queue<int> queue, Random randomGenerator, HashSet<int> notVisitedNodes)
        {
            if (notVisitedNodes.Count == 0) return;

            var randomSeed = randomGenerator.Next(0, notVisitedNodes.Count);
            var randomNode = notVisitedNodes.ElementAt(randomSeed);
            queue.Enqueue(randomNode);
        }
    }
}
