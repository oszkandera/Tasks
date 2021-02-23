using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace MAD2_Tasks.General.Code
{
    public class NetworkExporter
    {
        public void ExportToCsv(Dictionary<int, List<int>> network, string path)
        {
            using(var fStream = new FileStream(path, FileMode.Create))
            {
                using(var writer = new StreamWriter(fStream))
                {
                    foreach(var node in network)
                    {
                        foreach(var neighbor in node.Value)
                        {
                            writer.WriteLine($"{node.Key};{neighbor}");
                        }
                    }
                }
            }
        }

        public void ExportToGEXF(Dictionary<int, List<int>> network, string path)
        {
            var document = new XDocument();

            var graphElement = new XElement("graph", new XAttribute("mode", "static"), new XAttribute("defaultedgetype", "undirected"));

            var nodesElement = new XElement("nodes");

            foreach(var node in network)
            {
                var nodeElement = new XElement("node", new XAttribute("id", $"{node.Key}"));
                nodesElement.Add(nodeElement);
            }

            graphElement.Add(nodesElement);

            var edgesElement = new XElement("edges");

            int tempEdgeId = 0;
            foreach(var node in network)
            {
                foreach(var neighbor in node.Value)
                {
                    var edgeElement = new XElement("edge", 
                                                        new XAttribute("id", $"{tempEdgeId}"), 
                                                        new XAttribute("source", $"{node.Key}"), 
                                                        new XAttribute("target", $"{neighbor}"));
                    edgesElement.Add(edgeElement);
                    tempEdgeId++;
                }
            }

            graphElement.Add(edgesElement);

            var rootElement = new XElement("gexf");
            rootElement.Add(graphElement);

            document.Add(rootElement);

            document.Save(path);
        }
    }
}
