namespace MAD2_Tasks.General.Factory
{
    public static class ArrayFactory
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

        public static T[][] InitTwoDimensionalArray<T>(int sizeDimension1, int sizeDimension2)
        {
            var array = new T[sizeDimension1][];

            for (int i = 0; i < sizeDimension1; i++)
            {
                array[i] = new T[sizeDimension2];
            }
            return array;
        }
    }
}
