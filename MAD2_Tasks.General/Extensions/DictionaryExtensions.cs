using System.Collections.Generic;

namespace MAD2_Tasks.General.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Initialize(this Dictionary<int, List<int>> source, int size)
        {
            for (int i = 0; i < size; i++)
            {
                source.Add(i, new List<int>());
            }
        }
    }
}
