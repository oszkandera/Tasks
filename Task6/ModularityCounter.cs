namespace Task6
{
    public class ModularityCounter
    {
        public double CalculateModulairty(int[][] network, string[] classes)
        {
            var totalNumberOfEdges = GetNumberOfEdges(network);
            var nodesDegree = GetNodesDegree(network);

            var tempSum = 0;
            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    var kroneckerDelta = classes[i] == classes[j] ? 1 : 0;
                    var Aij = network[i][j];
                    var kikj = nodesDegree[i] * nodesDegree[j];

                    tempSum += (Aij - (kikj / (2 * totalNumberOfEdges))) * kroneckerDelta;
                }
            }

            return (1 / (double)(2 * totalNumberOfEdges)) * tempSum;
        }

        private int GetNumberOfEdges(int[][] network)
        {
            var numberOfEdges = 0;
            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    numberOfEdges += network[i][j];
                }
            }

            return numberOfEdges / 2;
        }

        private int[] GetNodesDegree(int[][] network)
        {
            var nodesDegree = new int[network.Length];

            for (int i = 0; i < network.Length; i++)
            {
                for (int j = 0; j < network.Length; j++)
                {
                    nodesDegree[i] += network[i][j];
                }
            }

            return nodesDegree;
        }
    }
}
