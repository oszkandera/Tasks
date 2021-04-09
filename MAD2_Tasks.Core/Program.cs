using Task1;
using MAD2_Tasks.General;
using MAD2_Tasks.General.Code;
using MAD2_Tasks.Core.Code;
using Task2;
using System.Linq;
using System.Collections.Generic;
using Task3;
using MAD2_Tasks.General.Enums;

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
            ModelGeneratorTest();
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
    }
}
