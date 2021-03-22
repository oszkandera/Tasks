namespace MAD2_Tasks.General.Factory
{
    public static class MatrixFactory
    {
        public static T[][] CreateMatrix<T>(int size, T defaultValue)
        {
            var adjacencyMatrix = new T[size][];

            for (int i = 0; i < size; i++)
            {
                adjacencyMatrix[i] = new T[size];
                for (int y = 0; y < size; y++)
                {
                    adjacencyMatrix[i][y] = defaultValue;
                }
            }
            return adjacencyMatrix;
        }
    }
}
