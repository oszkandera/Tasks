using System;

namespace MAD2_Tasks.General
{
    public class DistanceMatrixGenerator
    {
        public double[][] CreateFromVector(double[][] vectorData, Func<double[], double[], double> distanceFunction)
        {
            var numberOfInstances = vectorData.Length;
            var distanceMatrix = InitializeDistanceMatrix(numberOfInstances);

            for (int x = 0; x < vectorData.Length; x++)
            {
                for (int y = 0; y < vectorData.Length; y++)
                {
                    var distance = distanceFunction(vectorData[x], vectorData[y]);

                    distanceMatrix[x][y] = distance;
                }
            }

            return distanceMatrix;
        }

        private double[][] InitializeDistanceMatrix(int size, double value = 0)
        {
            var distanceMatrix = new double[size][];
            for (int i = 0; i < size; i++)
            {
                distanceMatrix[i] = new double[size];
                for (int y = 0; y < size; y++)
                {
                    distanceMatrix[i][y] = value;
                }
            }

            return distanceMatrix;
        }
    }
}
