using MAD2_Tasks.General.Extensions;
using System.Collections.Generic;
using System.IO;

namespace MAD2_Tasks.General.Code.Loaders
{
    public class MTXGraphLoader
    {
        public Dictionary<int, List<int>> Load(string filePath, string separator = " ")
        {
            var graph = new Dictionary<int, List<int>>();

            using (var reader = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                string line;
                SkipCommentAndInfoRows(reader);
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("%")) continue;

                    var nodes = line.Split(separator);

                    var nodeX = int.Parse(nodes[0]);
                    var nodeY = int.Parse(nodes[1]);

                    graph.AddBidirectEdge(nodeX, nodeY);
                }
            }

            if (!graph.ContainsKey(0))
            {
                var normalizedGraph = GetNormalizedGraph(graph);
                return normalizedGraph;
            }

            return graph;
        }


        private Dictionary<int, List<int>> GetNormalizedGraph(Dictionary<int, List<int>> graph)
        {
            var normalizedGraph = new Dictionary<int, List<int>>();
            foreach(var node in graph)
            {
                var normalizedNodeId = node.Key - 1;
                normalizedGraph.Add(normalizedNodeId, new List<int>());
                foreach(var neighbor in node.Value)
                {
                    var normalizedNeighborId = neighbor - 1;
                    normalizedGraph[normalizedNodeId].Add(normalizedNeighborId);
                }
            }

            return normalizedGraph;
        }

        private void SkipCommentAndInfoRows(StreamReader reader)
        {
            reader.ReadLine(); // First row is comment
            reader.ReadLine(); // Second row is row with information about number of nodes
        }
    }
}
