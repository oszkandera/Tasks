using System;

namespace MAD2_Tasks.Core.Algorithms
{
    public static class DistanceCalculator
    {
        public static double GetEuklidDistance(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Number of elements in first instance is not same like in sencond instance.");
            }

            double distance = 0;

            for (int i = 0; i < x.Length; i++)
            {
                distance += Math.Pow(x[i] - y[i], 2);
            }

            return Math.Sqrt(distance);
        }
    }
}
