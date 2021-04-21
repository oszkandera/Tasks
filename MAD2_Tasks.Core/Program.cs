using Task1;
using MAD2_Tasks.General;
using MAD2_Tasks.General.Code;
using MAD2_Tasks.Core.Code;
using Task2;
using System.Linq;
using System.Collections.Generic;
using Task3;
using MAD2_Tasks.General.Enums;
using Task4;
using MAD2_Tasks.General.Models;
using System.Text;
using Task5;
using Task6;
using MAD2_Tasks.General.Extensions;
using System.IO;
using System;
using MAD2_Tasks.General.Helpers;

namespace MAD2_Tasks.Core
{
    class Program
    {
        static void Main(string[] args)
        {

            //Task 1 - K
            //EpsilonRadiusTest();
            //KnnGeneratorTest();
            //EpsilonKnnGeneratorTest();

            //Task 2 - O
            //CommunityDetectionUsingHierarchicalClusteringTest();

            //Task 3 - K
            //ModelGeneratorTest();

            //Task 4 - K
            //AnalyerMultilayerGraph();

            //Task 5 - O
            //var network = GenerateGraphs();
            //ExportToCsv(network, Constants.EpsilonNetworkOutputCsvPath + "2");
            //ExportToGdf(network, Constants.EpsilonLouvinCommunities, Constants.EpsilonComunitiesOutputPath);
            //AnalyerMultilayerGraph();

            //Task 6 - O
            RunModularityTest();
        }

        #region Task 1 - K

        private static void EpsilonRadiusTest()
        {
            var epsilonRadisuAlgorithm = new EpsilonRadiusNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = epsilonRadisuAlgorithm.CreateNetwork(vectorData, 0.9);

            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.EpsilonGeneratedNetworkOutputPath);
        }

        private static void KnnGeneratorTest()
        {
            var knnGeneratorAlgorithm = new KnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = knnGeneratorAlgorithm.CreateNetwork(vectorData, 10);
            
            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.KnnGeneratedNetworkOutputPath);
        }

        private static void EpsilonKnnGeneratorTest()
        {
            var knnGeneratorAlgorithm = new EpsilonKnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = knnGeneratorAlgorithm.CreateNetwork(vectorData, 0.9, 3);
            
            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.EpsilonKnnGeneratedNetworkOutputPath);
        }

        #endregion

        #region Task 2 - O

        private static void CommunityDetectionUsingHierarchicalClusteringTest()
        {
            var comunityDetectionAlgorithm = new CommunityDetectionUsingHierarchicalClustering();

            var networkLoader = new AdjacencyListGraphLoader();
            var network = networkLoader.Load(Constants.KarateClubPath);


            var comunities = comunityDetectionAlgorithm.GetComunities(network, out double[][] similarityMatrix);

            //Generováni heatmapy
            //var stringToExcelBuilder = new StringBuilder();
            //for (int row = 0; row < similarityMatrix.Length; row++)
            //{
            //    var line = string.Join(";", similarityMatrix[row]);
            //    stringToExcelBuilder.AppendLine(line);
            //}
            //var heatMap = stringToExcelBuilder.ToString();

            var threeComunity = comunities.Where(x => x.Count == 3).FirstOrDefault();

            var networkWithComunities = comunityDetectionAlgorithm.GetNetworkWithComunityId(network, threeComunity);

            var comunityExporter = new ComunityExporter();

            var colorMap = new Dictionary<int, string>()
            {
                { 0, "#D34021" },
                { 1, "#58C11B" },
                { 2, "#1B60C1" }
            };

            comunityExporter.ExportToGDF(networkWithComunities, colorMap, Constants.KarateClubComunityExportPath);
        }

        #endregion

        #region Task 3 - K

        private static void ModelGeneratorTest()
        {
            var linkSelectionModelGenerator = new LinkSelectionModelGenerator();  
            var graphLinkSelection = linkSelectionModelGenerator.Generate(1000, 0.5, 0.1);

            var copyingModelGenerator = new CopyingModelGenerator();
            var graphCopyingModel = copyingModelGenerator.Generate(1000, 0.5, 0.1);

            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(graphLinkSelection, Constants.LinkSelectionModelExportPath);
            networkExporter.ExportToGEXF(graphCopyingModel, Constants.CopyingModelExportPath, EdgeType.Directed);
        }

        #endregion

        #region Task 4 - K

        private static void AnalyerMultilayerGraph()
        {
            var pathToSource = Constants.MultiLayerGraphSourcePath;

            var analyser = new MultilayerGraphAnalyser();
            var result = analyser.ProcessAnalysis(pathToSource, new int[] { 0, 1, 2 });

            var resultInCsv = CreateCsvString(result);
        }

        private static string CreateCsvString(MultilayerGraphAnalysisResult analysisResult)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine($"NodeId;Name;DegreeCentrality;DegreeDeviation;NeighborhoodCentrality;ConnectivityRedundandy;ExclusiveNeighborhood");

            for (int i = 0; i < analysisResult.Graph.RowValues.Length; i++)
            {
                csvBuilder.AppendLine($"{i};{analysisResult.Graph.RowValues[i]};{analysisResult.DegreeCentrality[i]};" + 
                                      $"{analysisResult.DegreeDeviation[i]};{analysisResult.NeighborhoodCentrality[i]};" + 
                                      $"{analysisResult.ConnectivityRedundancy[i]};{analysisResult.ExclusiveNeighborhood[i]}");

            }

            return csvBuilder.ToString();
        }

        #endregion

        #region Task5 - O

        private static Dictionary<int, List<int>> GenerateGraphs()
        {

            var epsilonRadisuAlgorithm = new EpsilonRadiusNetworkGenerator();
            var knnGeneratorAlgorithm = new KnnNetworkGenerator();
            var epsilonKnnGeneratorAlgorithm = new EpsilonKnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var networkLoader = new AdjacencyListGraphLoader();

            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var epsilonRadiusNetwork = epsilonRadisuAlgorithm.CreateNetwork(vectorData, 0.85);
            var knnNetwork = knnGeneratorAlgorithm.CreateNetwork(vectorData, 10);
            var epsilonKnnNetwork = epsilonKnnGeneratorAlgorithm.CreateNetwork(vectorData, 0.5, 9);
            var karateClubNetwork = networkLoader.Load(Constants.KarateClubPath);


            //return epsilonRadiusNetwork;
            //return epsilonKnnNetwork;
            //return knnNetwork;
            return karateClubNetwork;
        }


        private static void ExportToCsv(Dictionary<int, List<int>> network, string path)
        {
            var networkExporter = new NetworkExporter();

            networkExporter.ExportToCsv(network, path);
        }

        private static void ExportToGdf(Dictionary<int, List<int>> network, string pathToCommunities, string exportPath)
        {
            var task5 = new Task5Implementation();
            var louvinNetworkCommunities = task5.GetNetworkWithComunitiesFromCsv(network, pathToCommunities);

            var comunityExporter = new ComunityExporter();

            var colorMap = ColorMapHelper.GetColorMap();
            
            comunityExporter.ExportToGDF(louvinNetworkCommunities, colorMap, exportPath);
        }

        #endregion

        #region Task 6 - O

        public static void RunModularityTest()
        {
            var epsilonRadisuAlgorithm = new EpsilonRadiusNetworkGenerator();
            var knnAlgorithm = new KnnNetworkGenerator();
            var epsilonKnnAlgorithm = new EpsilonKnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var modularityCounter = new ModularityCounter();

            var (vectorData, classes) = vectorDataLoader.LoadDataWithClasses(Constants.IrisDataSetPath, 4, skipRowsNumber: 1);

            var epsilonRadiusNetwork = epsilonRadisuAlgorithm.CreateNetwork(vectorData, 0.85);
            var knnNetwork = knnAlgorithm.CreateNetwork(vectorData, 10);
            var epsilonKnnNetwork = epsilonKnnAlgorithm.CreateNetwork(vectorData, 0.5, 9);

            #region Original modularity

            var adjacencyMatrixEpsilon = epsilonRadiusNetwork.ToAdjacencyMatrix();
            var adjacencyMatrixKnn = knnNetwork.ToAdjacencyMatrix();
            var adjacencyMatrixEpsilonKnn = epsilonKnnNetwork.ToAdjacencyMatrix();

            var originalEpsilonModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilon, classes);
            var originalKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixKnn, classes);
            var originalEpsilonKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilonKnn, classes);

            #endregion

            #region InfoMap

            var epsilonInfoMapClasses = LoadClasses(Constants.EpsilonInfoMapClassesPath);
            var knnInfoMapClasses = LoadClasses(Constants.KnnInfoMapClassesPath);
            var epsilonKnnInfoMapClasses = LoadClasses(Constants.EpsilonKnnInfoMapClassesPath);

            var infoMapEpsilonModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilon, epsilonInfoMapClasses);
            var infoMapKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixKnn, knnInfoMapClasses);
            var infoMapEpsilonKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilonKnn, epsilonKnnInfoMapClasses);

            #endregion

            #region LabelProp

            var epsilonLabelPropClasses = LoadClasses(Constants.EpsilonLabelPropClassesPath);
            var knnLabelPropClasses = LoadClasses(Constants.KnnLabelPropClassesPath);
            var epsilonKnnLabelPropClasses = LoadClasses(Constants.EpsilonKnnLabelPropClassesPath);

            var LabelPropEpsilonModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilon, epsilonLabelPropClasses);
            var LabelPropKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixKnn, knnLabelPropClasses);
            var LabelPropEpsilonKnnModularity = modularityCounter.CalculateModulairty(adjacencyMatrixEpsilonKnn, epsilonKnnLabelPropClasses);

            #endregion

            #region Visualisation

            var networkClassConnector = new NetworkClassConnector();
            var epsilonNetworkWithClasses = networkClassConnector.GetNetworkWithClasses(epsilonRadiusNetwork, classes);
            var knnNetworkWithClasses = networkClassConnector.GetNetworkWithClasses(knnNetwork, classes);
            var epsilonKnnNetworkWithClasses = networkClassConnector.GetNetworkWithClasses(epsilonKnnNetwork, classes);

            var colorMap = ColorMapHelper.GetColorMap();
            var comunityExporter = new ComunityExporter();
            comunityExporter.ExportToGDF(epsilonNetworkWithClasses, colorMap, Constants.EpsilonNetworkWithClassesPath);
            comunityExporter.ExportToGDF(knnNetworkWithClasses, colorMap, Constants.KnnNetworkWithClassesPath);
            comunityExporter.ExportToGDF(epsilonKnnNetworkWithClasses, colorMap, Constants.EpsilonKnnNetworkWithClassesPath);

            #endregion
        }

        private static string[] LoadClasses(string pathToFileWithClasses)
        {
            var data = File.ReadAllLines(pathToFileWithClasses);

            var classes = new string[150];

            foreach(var line in data)
            {
                var values = line.Split(";");
                if (values.Length != 2) continue;
                if (String.IsNullOrWhiteSpace(values[0]) || String.IsNullOrWhiteSpace(values[0])) continue;

                var nodeIdx = int.Parse(values[0]);
                var @class = values[1];

                classes[nodeIdx] = @class;
            }

            return classes;
        }

        #endregion
    }
}
