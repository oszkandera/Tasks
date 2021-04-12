using System.Collections.Generic;

namespace MAD2_Tasks.General.Models
{
    public class MultilayerGraph
    {
        public string[] RowValues { get; set; }
        public string[] ColValues { get; set; }
        public List<bool[][]> Graph { get; set; }
        public int LayerCount { get; set; }
    }
}
