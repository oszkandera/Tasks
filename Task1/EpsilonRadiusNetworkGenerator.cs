using MAD2_Tasks.Core.Algorithms;
using MAD2_Tasks.General;
using MAD2_Tasks.General.Extensions;
using System.Collections.Generic;

namespace Task1
{
    public class EpsilonRadiusNetworkGenerator
    {
        public Dictionary<int, List<int>> CreateNetwork(double[][] vectorData, double epsilon)
        {
            var distanceMatrixGenerator = new DistanceMatrixGenerator();

            var distanceMatrix = distanceMatrixGenerator.CreateFromVector(vectorData, DistanceCalculator.GetEuklidDistance);

            var network = new Dictionary<int, List<int>>();
            network.Initialize(distanceMatrix.Length);

            for (int x = 0; x < distanceMatrix.Length; x++)
            {
                for (int y = x + 1; y < distanceMatrix.Length; y++)
                {
                    if(distanceMatrix[x][y] <= epsilon)
                    {
                        network[x].Add(y);
                        network[y].Add(x);
                    }
                }
            }

            return network;
        }
    }
}
