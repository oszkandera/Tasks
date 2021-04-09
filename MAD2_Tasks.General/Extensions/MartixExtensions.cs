using MAD2_Tasks.General.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD2_Tasks.General.Extensions
{
    public static class MartixExtensions
    {
        public static double[][] ToSimilarityMatrix(this int[][] adjacencyMatrix)
        {
            var size = adjacencyMatrix.Length;
            var similarityMatrix = ArrayFactory.CreateMatrix(size, 0.0);

            var nodeDegres = GetDegreeOfNodes(adjacencyMatrix);

            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                for (int y = 0; y < adjacencyMatrix.Length; y++)
                {
                    if(i == y) { similarityMatrix[i][y] = 1; }

                    var numberOfSameNeighbors = 0;

                    for (int z = 0; z < adjacencyMatrix.Length; z++)
                    {
                        if (adjacencyMatrix[i][z] == 1 && adjacencyMatrix[y][z] == 1) { numberOfSameNeighbors++; }
                    }

                    similarityMatrix[i][y] = similarityMatrix[y][i]= numberOfSameNeighbors / Math.Sqrt(nodeDegres[i] * nodeDegres[y]);
                }
            }


            return similarityMatrix;
        }

        private static int[] GetDegreeOfNodes(int[][] adjacencyMatrix)
        {
            var degrees = new int[adjacencyMatrix.Length];
            
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                var degree = 0;
                for (int y = 0; y < adjacencyMatrix[i].Length; y++)
                {
                    if (adjacencyMatrix[i][y] == 1) { degree++; };
                }

                degrees[i] = degree;
            }

            return degrees;
        }
    }
}
