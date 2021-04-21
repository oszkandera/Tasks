using MAD2_Tasks.General.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task5
{
    public class Task5Implementation
    {
        public Dictionary<int, Tuple<int, List<int>>> GetNetworkWithComunitiesFromCsv(Dictionary<int, List<int>> network, string pathToCsvFileWithComunities)
        {
            var comunityJurisdiction = new Dictionary<int, int>();

            using (var stream = new FileStream(pathToCsvFileWithComunities, FileMode.Open))
            {
                bool isFirst = true;
                using (var reader = new StreamReader(stream))
                {
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            continue;
                        }

                        var data = line.Split(";");
                        comunityJurisdiction.Add(int.Parse(data[0]), int.Parse(data[1]));
                    }
                }
            }

            var networkWithComunityId = new Dictionary<int, Tuple<int, List<int>>>();


            network.RemoveParalelEdges();
            foreach (var node in network)
            {
                var comunityId = comunityJurisdiction.FirstOrDefault(x => x.Key == node.Key).Value;
                networkWithComunityId.Add(node.Key, new Tuple<int, List<int>>(comunityId, node.Value));
            }

            return networkWithComunityId;
        }
    }
}
