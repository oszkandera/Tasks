using MAD2_Tasks.General.Factory;
using MAD2_Tasks.General.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MAD2_Tasks.General.Code
{
    public class FullMatrixDataLoader
    {
        public MultilayerGraph LoadMultiLayerGraph(string path)
        {
            var multiLayerGraph = new List<bool[][]>();
            var lines = File.ReadAllLines(path);

            int numberOfNodes = 0;
            int numberOfLayers = 0;
            string[] rowsName = null;
            string[] colsName = null;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.StartsWith("N=")) numberOfNodes = Convert.ToInt32(line.Split('=')[1]);
                if (line.StartsWith("NM=")) numberOfLayers = Convert.ToInt32(line.Split('=')[1]);

                if (line.Trim().Equals("ROW LABELS:"))
                {
                    i++;
                    if(numberOfNodes == 0) break;
                    rowsName = new string[numberOfNodes];

                    int tempRowIndex = 0;
                    var rowsLength = i + numberOfNodes;
                    for (; i < rowsLength; i++)
                    {
                        rowsName[tempRowIndex] = lines[i];
                        tempRowIndex++;
                    }
                    continue;
                }

                if (line.Trim().Equals("COLUMN LABELS:"))
                {
                    i++;
                    if (numberOfNodes == 0) break;
                    colsName = new string[numberOfNodes];

                    int tempColIndex = 0;
                    int colsLength = i + numberOfNodes;
                    for (; i < i + colsLength; i++)
                    {
                        colsName[tempColIndex] = lines[i];
                        tempColIndex++;
                    }
                    continue;
                }

                if (line.Trim().Equals("DATA:"))
                {
                    if (numberOfLayers == 0) break;
                    for (int y = 0; y < numberOfLayers; y++)
                    {
                        var graph = ArrayFactory.InitTwoDimensionalArray<bool>(numberOfNodes, numberOfNodes);

                        for (int r = 0; r < numberOfNodes; r++)
                        {
                            i++;
                            var colValues = lines[i].Split(" ");
                            //if (colValues.Length < numberOfNodes) continue;
                            for (int c = 0; c < numberOfNodes; c++)
                            {
                                graph[r][c] = colValues[c] == "1" ? true : false;
                            }
                        }

                        multiLayerGraph.Add(graph);
                    }
                    continue;
                }
            }

            return new MultilayerGraph()
            {
                ColValues = colsName,
                RowValues = rowsName,
                Graph = multiLayerGraph,
                LayerCount = numberOfLayers
            };
        }
    }
}
