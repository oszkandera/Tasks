using System;
using System.Collections.Generic;
using System.IO;

namespace MAD2_Tasks.General.Code
{
    public class ComunityExporter
    {
        public void ExportToGDF(Dictionary<int, Tuple<int, List<int>>> networkWithComunityId, Dictionary<int, string> colorMap, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                using(var writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("nodedef>name VARCHAR,color VARCHAR");
                    foreach(var node in networkWithComunityId)
                    {
                        writer.WriteLine($"{node.Key}, {colorMap[node.Value.Item1]}");
                    }

                    writer.WriteLine("edgedef>node1 VARCHAR,node2 VARCHAR");
                    foreach(var node in networkWithComunityId)
                    {
                        foreach(var neighbor in node.Value.Item2)
                        {
                            writer.WriteLine($"{node.Key},{neighbor}");
                        }
                    }
                }
            }
        }
    }
}
