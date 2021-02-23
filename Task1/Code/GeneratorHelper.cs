using System;
using System.Collections.Generic;

namespace Task1.Code
{
    public class GeneratorHelper
    {
        public void AddBidirectEdgeIfNotExists(Dictionary<int, List<int>> network, int node, int neighbor)
        {
            if (!network[node].Contains(neighbor))
            {
                network[node].Add(neighbor);
            }

            if (!network[neighbor].Contains(node))
            {
                network[neighbor].Add(node);
            }
        }

        public void AddOrUpdateNearestPoints(List<Tuple<int, double>> nearestPoints, Tuple<int, double> newPoint, int k)
        {
            if (nearestPoints.Count < k)
            {
                nearestPoints.Add(newPoint);
                return;
            }

            var farthestPoint = new Tuple<int, double>(-1, 0);

            foreach (var nearestPoint in nearestPoints)
            {
                if (nearestPoint.Item2 > farthestPoint.Item2)
                {
                    farthestPoint = nearestPoint;
                }
            }

            if (farthestPoint.Item1 != -1 && farthestPoint.Item2 > newPoint.Item2)
            {
                nearestPoints.Remove(farthestPoint);
                nearestPoints.Add(newPoint);
            }
        }
    }
}
