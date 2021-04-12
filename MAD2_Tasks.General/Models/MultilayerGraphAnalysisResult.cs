using System.Collections.Generic;

namespace MAD2_Tasks.General.Models
{
    public class MultilayerGraphAnalysisResult
    {
        public MultilayerGraph Graph { get; set; }
        public Dictionary<int, int> DegreeCentrality { get; set; }
        public Dictionary<int, double> DegreeDeviation { get; set; }
        public Dictionary<int, int> NeighborhoodCentrality { get; set; }
        public Dictionary<int, double> ConnectivityRedundancy { get; set; }
        public Dictionary<int, int> ExclusiveNeighborhood { get; set; }
    }
}
