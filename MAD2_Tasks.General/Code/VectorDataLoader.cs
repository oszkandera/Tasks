using System;
using System.IO;
using System.Linq;

namespace MAD2_Tasks.General
{
    public class VectorDataLoader
    {
        public double[][] LoadData(string filePath, string separator = ";", int skipRowsNumber = 0, int[] columnsToSkip = null)
        {
            var rawData = File.ReadAllLines(filePath);

            var numberOfLines = rawData.Length;
            var data = new double[numberOfLines - skipRowsNumber][];

            var numberOfColumnsToSkip = columnsToSkip == null ? 0 : columnsToSkip.Length;

            var tempRowIndex = 0;
            for (int r = skipRowsNumber; r < numberOfLines; r++)
            {
                var row = rawData[r];
                var columns = row.Split(separator);

                var numberOfColumns = columns.Length - numberOfColumnsToSkip;

                data[tempRowIndex] = new double[numberOfColumns];

                int tempColumnIndex = 0;
                for (int c = 0; c < columns.Length; c++)
                {
                    if (!columnsToSkip.Contains(c))
                    {
                        if(Double.TryParse(columns[c], out double value))
                        {
                            data[tempRowIndex][tempColumnIndex] = value;
                        }

                        tempColumnIndex++;
                    }
                }
                tempRowIndex++;
            }

            return data;
        }
    }
}
