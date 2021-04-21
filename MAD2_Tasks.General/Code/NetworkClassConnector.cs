using System;
using System.Collections.Generic;

namespace MAD2_Tasks.General.Code
{
    public class NetworkClassConnector
    {
        public Dictionary<int, Tuple<int, List<int>>> GetNetworkWithClasses(Dictionary<int, List<int>> network, string[] classes)
        {
            var networkWithClasses = new Dictionary<int, Tuple<int, List<int>>>();

            var classesInIntFormat = GetClassesInIntFormat(classes);

            foreach (var node in network)
            {
                var @class = classesInIntFormat[node.Key];
                networkWithClasses.Add(node.Key, new Tuple<int, List<int>>(@class, node.Value));
            }

            return networkWithClasses;
        }

        private int[] GetClassesInIntFormat(string[] classes)
        {
            var uniqueClasses = GetUniquesClassesWithIds(classes);

            var normalizedClasses = new int[classes.Length];

            for (int i = 0; i < classes.Length; i++)
            {
                var @class = classes[i];
                normalizedClasses[i] = uniqueClasses[@class];
            }

            return normalizedClasses;
        }

        private Dictionary<string, int> GetUniquesClassesWithIds(string[] classes)
        {
            var uniqueClasses = new Dictionary<string, int>();
            var uniqueIdx = 0;
            foreach (var @class in classes)
            {
                if (uniqueClasses.ContainsKey(@class)) continue;

                uniqueClasses.Add(@class, uniqueIdx);
                uniqueIdx++;
            }

            return uniqueClasses;
        }
    }
}
