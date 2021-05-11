using MAD2_Tasks.General.Algorithms;
using MAD2_Tasks.General.Code;
using MAD2_Tasks.General.Code.Loaders;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{    
    public partial class DetailWindow : Window
    {
        private readonly Dictionary<int, List<int>> _originalGraph;
        private readonly Dictionary<int, List<int>> _sampleGraph;
        public DetailWindow(Dictionary<int, List<int>> originalGraph, Dictionary<int, List<int>> sampleGraph, TimeSpan processingTime)
        {
            InitializeComponent();

            _originalGraph = originalGraph;
            _sampleGraph = sampleGraph;


            var generalGraphAnalyzer = new GeneralGraphAnalyser();
            var originalGraphNumberOfNodes = generalGraphAnalyzer.GetNumberOfNodes(originalGraph);
            var originalGraphNumberOfEdges = generalGraphAnalyzer.GetNumberOfEdges(originalGraph);
            var originalGraphAverageDegree = generalGraphAnalyzer.GetAverageDegree(originalGraph);
            var originalGraphMaxDegree = generalGraphAnalyzer.GetMaxDegree(originalGraph);
            var originalGraphAverageClusteringCoeficient = generalGraphAnalyzer.GetAverageClusteringCoeficient(originalGraph);
            
            var originalGraphComponents = generalGraphAnalyzer.GetComponents(originalGraph);
            var originalGraphNumberOfComponentsWithAtLeastTwoNodes = originalGraphComponents.Where(x => x.Count >= 2).Count();
            var originalGraphNumberOfIsolatedNodes = originalGraphComponents.Where(x => x.Count == 1).Count();
            var originalGraphSizeOfBigestComponent = originalGraphComponents.Max(x => x.Count);

            var sampleGraphNumberOfNodes = generalGraphAnalyzer.GetNumberOfNodes(sampleGraph);
            var sampleGraphNumberOfEdges = generalGraphAnalyzer.GetNumberOfEdges(sampleGraph);
            var sampleGraphAverageDegree = generalGraphAnalyzer.GetAverageDegree(sampleGraph);
            var sampleGraphMaxDegree = generalGraphAnalyzer.GetMaxDegree(sampleGraph);
            var sampleGraphAverageClusteringCoeficient = generalGraphAnalyzer.GetAverageClusteringCoeficient(sampleGraph);

            var sampleGraphComponents = generalGraphAnalyzer.GetComponents(sampleGraph);
            var sampleGraphNumberOfComponentsWithAtLeastTwoNodes = sampleGraphComponents.Where(x => x.Count >= 2).Count();
            var sampleGraphNumberOfIsolatedNodes = sampleGraphComponents.Where(x => x.Count == 1).Count();
            var sampleGraphSizeOfBigestComponent = sampleGraphComponents.Max(x => x.Count);


            OriginalGraphNumberOfNodesTextBox.Text = originalGraphNumberOfNodes.ToString();
            OriginalGraphNumberOfEdgesTextBox.Text = originalGraphNumberOfEdges.ToString();
            OriginalGraphAverageDegreeTextBox.Text = originalGraphAverageDegree.ToString();
            OriginalGraphMaxDegreeTextBox.Text = originalGraphMaxDegree.ToString();
            OriginalGraphAverageClusteringCoeficientTextBox.Text = originalGraphAverageClusteringCoeficient.ToString();
            OriginalGraphNumberOfComponentsWithTwoAndMoreComponentsTextBox.Text = originalGraphNumberOfComponentsWithAtLeastTwoNodes.ToString();
            OriginalGraphNumberOfIsolatedNodesTextBox.Text = originalGraphNumberOfIsolatedNodes.ToString();
            OriginalGraphSizeOfBiggestComponentTextBox.Text = originalGraphSizeOfBigestComponent.ToString();

            SampleGraphNumberOfNodesTextBox.Text = sampleGraphNumberOfNodes.ToString();
            SampleGraphNumberOfEdgesTextBox.Text = sampleGraphNumberOfEdges.ToString();
            SampleGraphAverageDegreeTextBox.Text = sampleGraphAverageDegree.ToString();
            SampleGraphMaxDegreeTextBox.Text = sampleGraphMaxDegree.ToString();
            SampleGraphAverageClusteringCoeficientTextBox.Text = sampleGraphAverageClusteringCoeficient.ToString();
            SampleGraphNumberOfComponentsWithTwoAndMoreComponentsTextBox.Text = sampleGraphNumberOfComponentsWithAtLeastTwoNodes.ToString();
            SampleGraphNumberOfIsolatedNodesTextBox.Text = sampleGraphNumberOfIsolatedNodes.ToString();
            SampleGraphSizeOfBiggestComponentTextBox.Text = sampleGraphSizeOfBigestComponent.ToString();

            ProcessingTimeTextBox.Text = processingTime.ToString();
        }


    }
}
