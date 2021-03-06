﻿namespace MAD2_Tasks.Core.Code
{
    public static class Constants
    {
        #region Task 1
        
        public static readonly string IrisDataSetPath = "../../../Data/iris.csv";
        public static readonly string EpsilonGeneratedNetworkOutputPath = "../../../../Task1/Output/epsilonGeneratedNetwork.gexf";
        public static readonly string KnnGeneratedNetworkOutputPath = "../../../../Task1/Output/knnGeneratedNetwork.gexf";
        public static readonly string EpsilonKnnGeneratedNetworkOutputPath = "../../../../Task1/Output/epsilonKnnGeneratedNetwork.gexf";
        #endregion

        #region Task 2
        
        public static readonly string KarateClubPath = "../../../Data/KarateClub.csv";
        public static readonly string KarateClubComunityExportPath = "../../../../Task2/Output/karateClubComunity.gdf";

        #endregion

        #region Task3

        public static readonly string LinkSelectionModelExportPath = "../../../../Task3/Output/linkSelectionModel.gexf";
        public static readonly string CopyingModelExportPath = "../../../../Task3/Output/copyingModel.gexf";

        #endregion

        #region Task4

        public static readonly string MultiLayerGraphSourcePath = "../../../Data/tailorshop.dat";

        #endregion

        #region Task5

        public static readonly string EpsilonNetworkOutputCsvPath = "../../../../Task5/Output/epsilonNetwork.csv";
        public static readonly string KnnNetworkOutputCsvPath = "../../../../Task5/Output/knnNetwork.csv";
        public static readonly string EpsilonKnnNetworkOutputCsvPath = "../../../../Task5/Output/epsilonKnnNetwork.csv";

        public static readonly string EpsilonComunitiesOutputPath = "../../../../Task5/Output/epsilonComunities.gdf";
        public static readonly string KnnComunitiesOutputPath = "../../../../Task5/Output/knnComunities.gdf";
        public static readonly string EpsilonKnnComunitiesOutputPath = "../../../../Task5/Output/epsilonKnnComunities.gdf";

        public static readonly string EpsilonLouvinCommunities = "../../../../Task5/Output/Analysis/exports/epsilonLouvainCluster.csv";
        public static readonly string KnnLouvinCommunities = "../../../../Task5/Output/Analysis/exports/knnLouvainCluster.csv";
        public static readonly string EpsilonKnnLouvinCommunities = "../../../../Task5/Output/Analysis/exports/epsilonKnnLouvainCluster.csv";

        #endregion

        #region Task 6

        public static readonly string EpsilonInfoMapClassesPath = "../../../../Task6/Input/EpsilonRadius/infoMapCluster.csv";
        public static readonly string EpsilonLabelPropClassesPath = "../../../../Task6/Input/EpsilonRadius/labelPropCluster.csv";

        public static readonly string KnnInfoMapClassesPath = "../../../../Task6/Input/Knn/infoMapCluster.csv";
        public static readonly string KnnLabelPropClassesPath = "../../../../Task6/Input/Knn/labelPropCluster.csv";

        public static readonly string EpsilonKnnInfoMapClassesPath = "../../../../Task6/Input/EpsilonKnn/infoMapCluster.csv";
        public static readonly string EpsilonKnnLabelPropClassesPath = "../../../../Task6/Input/EpsilonKnn/labelPropCluster.csv";

        
        public static readonly string EpsilonNetworkWithClassesPath = "../../../../Task6/Output/epsilonWithClasses.gdf";
        public static readonly string KnnNetworkWithClassesPath = "../../../../Task6/Output/knnWithClasses.gdf";
        public static readonly string EpsilonKnnNetworkWithClassesPath = "../../../../Task6/Output/epsilonKnnWithClasses.gdf";

        #endregion
    }
}
