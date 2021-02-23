using MAD2_Tasks.Core.Algorithms;
using MAD2_Tasks.General;
using MAD2_Tasks.General.Extensions;
using System;
using System.Collections.Generic;
using Task1.Code;

namespace Task1
{
    public class KnnNetworkGenerator
    {
        public Dictionary<int, List<int>> CreateNetwork(double[][] vectorData, int k)
        {
            var distanceMatrixGenerator = new DistanceMatrixGenerator();
            var generatorHelper = new GeneratorHelper();

            var distanceMatrix = distanceMatrixGenerator.CreateFromVector(vectorData, DistanceCalculator.GetEuklidDistance);

            var network = new Dictionary<int, List<int>>();
            network.Initialize(distanceMatrix.Length);

            for (int x = 0; x < distanceMatrix.Length; x++)
            {
                var nearestPoints = new List<Tuple<int, double>>();
                for (int y = 0; y < distanceMatrix.Length; y++)
                {
                    if (x == y) continue;
                    generatorHelper.AddOrUpdateNearestPoints(nearestPoints, new Tuple<int, double>(y, distanceMatrix[x][y]), k);
                }

                foreach (var nearestPoint in nearestPoints)
                {
                    generatorHelper.AddBidirectEdgeIfNotExists(network, x, nearestPoint.Item1);
                }
            }

            return network;
        }
    }
}
