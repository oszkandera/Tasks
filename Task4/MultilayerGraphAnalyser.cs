using MAD2_Tasks.General.Code;
using MAD2_Tasks.General.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    public class MultilayerGraphAnalyser
    {
        public MultilayerGraphAnalysisResult ProcessAnalysis(string path, int[] consideredLayers)
        {
            var loader = new FullMatrixDataLoader();
            var multiLayerGraphInfo = loader.LoadMultiLayerGraph(path);

            var neighborsOfNodesPerLayer = GetNeighborsOfNodePerLayer(multiLayerGraphInfo.Graph);
            var degreeCentralityByLayers = GetDegreeCentralityByLayers(neighborsOfNodesPerLayer);

            var degreeCentrality = GetDegreeCentrality(degreeCentralityByLayers, consideredLayers);
            var degreeDeviation = GetDegreeDeviationForAllNodes(degreeCentralityByLayers, consideredLayers);
            var neighborhoodCentrality = GetNeighborhoodCentrality(neighborsOfNodesPerLayer, consideredLayers);

            var connectivityRedundancy = GetConectivityRedundancy(neighborhoodCentrality, degreeCentrality);

            var exclusiveNeighborhood = GetExclusiveNeighborhood(neighborsOfNodesPerLayer, consideredLayers, multiLayerGraphInfo.LayerCount);

            return new MultilayerGraphAnalysisResult
            {
                Graph = multiLayerGraphInfo,
                DegreeCentrality = degreeCentrality,
                DegreeDeviation = degreeDeviation,
                NeighborhoodCentrality = neighborhoodCentrality,
                ConnectivityRedundancy = connectivityRedundancy,
                ExclusiveNeighborhood = exclusiveNeighborhood
            };
        }

        #region Degree centrality

        private Dictionary<int, List<int>> GetDegreeCentralityByLayers(Dictionary<int, HashSet<int>[]> neighborsPerNode)
        {
            var degreeCentrality = new Dictionary<int, List<int>>();

            foreach(var node in neighborsPerNode)
            {
                degreeCentrality[node.Key] = node.Value.Select(x => x.Count).ToList();
            }

            return degreeCentrality;
        }

        private Dictionary<int, int> GetDegreeCentrality(Dictionary<int, List<int>> degreeCentralityByLayers, int[] consideredLayers)
        {
            var degreeCentrality = new Dictionary<int, int>();

            foreach (var node in degreeCentralityByLayers)
            {
                var degreeSum = 0;
                for (int layerId = 0; layerId < consideredLayers.Length; layerId++)
                {
                    var layer = consideredLayers[layerId];
                    degreeSum += node.Value[layer];
                }

                degreeCentrality[node.Key] = degreeSum;
            }

            return degreeCentrality;
        }

        #endregion

        #region Degree deviation

        private Dictionary<int, double> GetDegreeDeviationForAllNodes(Dictionary<int, List<int>> degreeCentralityByLayers, int[] consideredLayers)
        {
            var degreeDeviation = new Dictionary<int, double>();

            for (int nodeId = 0; nodeId < degreeCentralityByLayers.Count; nodeId++)
            {
                var degrees = GetDegreesByLayer(degreeCentralityByLayers[nodeId], consideredLayers);
                var degreeDeviationSum = 0.0;
                var degreesSum = Enumerable.Sum(degrees);

                for (int i = 0; i < degrees.Count; i++)
                {
                    degreeDeviationSum += Math.Pow(degrees[i] - (degreesSum / (double)degrees.Count), 2);
                }

                var nodeDegreeDeviation = Math.Sqrt(degreeDeviationSum / degrees.Count);

                degreeDeviation[nodeId] = nodeDegreeDeviation;
            }

            return degreeDeviation;
        }

        private List<int> GetDegreesByLayer(List<int> degrees, int[] consideredLayers)
        {
            var degreesByLayer = new List<int>();

            for (int i = 0; i < consideredLayers.Length; i++)
            {
                var layer = consideredLayers[i];
                degreesByLayer.Add(degrees[layer]);
            }

            return degreesByLayer;
        }

        #endregion

        #region Neighborhood centrality

        private Dictionary<int, HashSet<int>[]> GetNeighborsOfNodePerLayer(List<bool[][]> graph)
        {
            var neighborsOfNodePerLayer = new Dictionary<int, HashSet<int>[]>();

            var numberOfLayers = graph.Count;

            for (int layerId = 0; layerId < numberOfLayers; layerId++)
            {
                var layer = graph[layerId];
                for (int nodeId = 0; nodeId < layer.Length; nodeId++)
                {
                    var node = layer[nodeId];
                
                    var neighborsPerLayer = new HashSet<int>();

                    for (int neighborId = 0; neighborId < node.Length; neighborId++)
                    {
                        if(node[neighborId]) neighborsPerLayer.Add(neighborId);
                    }

                    AddNeighborsOfNodeForCurrentLayer(neighborsOfNodePerLayer, neighborsPerLayer, layerId, nodeId, numberOfLayers);
        
                }
            }

            return neighborsOfNodePerLayer;
        }

        private void AddNeighborsOfNodeForCurrentLayer(Dictionary<int, HashSet<int>[]> neighborsOfNodePerLayer, 
            HashSet<int> neighbors, int layer, int node, int numberOfLayers)
        {
            if (!neighborsOfNodePerLayer.ContainsKey(node))
            {
                neighborsOfNodePerLayer[node] = new HashSet<int>[numberOfLayers];
            }
               
            neighborsOfNodePerLayer[node][layer] = neighbors;
        }

        private Dictionary<int, int> GetNeighborhoodCentrality(Dictionary<int, HashSet<int>[]> neighborsPerNode, int[] consideredLayers)
        {
            var neighborhoodCentrality = new Dictionary<int, int>();

            foreach(var nodeInfo in neighborsPerNode)
            {
                neighborhoodCentrality[nodeInfo.Key] = GetNeighborsOfConsideredLayers(neighborsPerNode, consideredLayers, nodeInfo.Key).Count;
            }

            return neighborhoodCentrality;
        }

        #endregion

        #region Connectivity redundancy

        private Dictionary<int, double> GetConectivityRedundancy(Dictionary<int, int> neighborhoodCentrality, Dictionary<int, int> degreeCentrality)
        {
            if(neighborhoodCentrality.Count != degreeCentrality.Count)
            {
                throw new ArgumentException($"Number of elements in parameter {nameof(neighborhoodCentrality)} not " +
                                            $"coresponding with number of elements in parameter {nameof(degreeCentrality)}");
            }

            var conectivityRedundancy = new Dictionary<int, double>();

            for (int i = 0; i < neighborhoodCentrality.Count; i++)
            {
                conectivityRedundancy[i] = 1 - (neighborhoodCentrality[i] / (double)degreeCentrality[i]);
            }

            return conectivityRedundancy;
        }

        #endregion

        #region Exclusive neighborhood

        private Dictionary<int, int> GetExclusiveNeighborhood(Dictionary<int, HashSet<int>[]> neighborsOfNodesPerLayer, 
            int[] consideredLayers, int numberOfLayers)
        {
            var exclusiveNeighborhood = new Dictionary<int, int>();

            var notConsideredLayers = GetNotConsideredLayers(numberOfLayers, consideredLayers);

            foreach (var nodeInfo in neighborsOfNodesPerLayer)
            {
                var neighborsOfConsideredLayers = GetNeighborsOfConsideredLayers(neighborsOfNodesPerLayer, consideredLayers, nodeInfo.Key);
                var neighborsOfNotConsideredLayers = GetNeighborsOfConsideredLayers(neighborsOfNodesPerLayer, notConsideredLayers, nodeInfo.Key);

                var excludedNodes = neighborsOfConsideredLayers.Except(neighborsOfNotConsideredLayers);

                exclusiveNeighborhood[nodeInfo.Key] = excludedNodes.Count();
            }

            return exclusiveNeighborhood;
        }

        private int[] GetNotConsideredLayers(int numberOfLayers, int[] consideredLayers)
        {
            var notConsideredLayers = new int[numberOfLayers - consideredLayers.Length];

            int tempIndex = 0;
            for (int i = 0; i < numberOfLayers; i++)
            {
                if (!consideredLayers.Contains(i))
                {
                    notConsideredLayers[tempIndex] = i;
                    tempIndex++;
                }
            }

            return notConsideredLayers;
        }

        private HashSet<int> GetNeighborsOfConsideredLayers(Dictionary<int, HashSet<int>[]> neighborsOfNodesPerLayer, int[] consideredLayers, int nodeId)
        {
            var neighbors = new HashSet<int>();

            var nodeInfo = neighborsOfNodesPerLayer[nodeId];
            for (int layerId = 0; layerId < consideredLayers.Length; layerId++)
            {
                var layer = consideredLayers[layerId];

                foreach (var neighbor in nodeInfo[layer])
                {
                    neighbors.Add(neighbor);
                }
            }
            

            return neighbors;
        }

        #endregion
    }
}
