using Task1;
using MAD2_Tasks.General;
using MAD2_Tasks.General.Code;
using MAD2_Tasks.Core.Code;

namespace MAD2_Tasks.Core
{
    class Program
    {       
        static void Main(string[] args)
        {
            //Task 1 - K
            EpsilonRadiusTest();
            KnnGeneratorTest();
            EpsilonKnnGeneratorTest();

        }

        #region Task 1

        private static void EpsilonRadiusTest()
        {
            var epsilonRadisuAlgorithm = new EpsilonRadiusNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = epsilonRadisuAlgorithm.CreateNetwork(vectorData, 0.3);

            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.EpsilonGeneratedNetworkOutputPath);
        }

        private static void KnnGeneratorTest()
        {
            var knnGeneratorAlgorithm = new KnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = knnGeneratorAlgorithm.CreateNetwork(vectorData, 1);
            
            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.KnnGeneratedNetworkOutputPath);
        }

        private static void EpsilonKnnGeneratorTest()
        {
            var knnGeneratorAlgorithm = new EpsilonKnnNetworkGenerator();
            var vectorDataLoader = new VectorDataLoader();
            var vectorData = vectorDataLoader.LoadData(Constants.IrisDataSetPath, skipRowsNumber: 1, columnsToSkip: new int[] { 4 });

            var network = knnGeneratorAlgorithm.CreateNetwork(vectorData, 0.9, 1);
            
            var networkExporter = new NetworkExporter();
            networkExporter.ExportToGEXF(network, Constants.EpsilonKnnGeneratedNetworkOutputPath);
        }

        #endregion
    }
}
