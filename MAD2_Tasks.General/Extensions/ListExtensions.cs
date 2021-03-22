using System.Collections.Generic;

namespace MAD2_Tasks.General.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> Clone<T>(this List<List<T>> source)
        {
            var newList = new List<List<T>>();

            foreach(var values in source)
            {
                var innerList = new List<T>();
                innerList.AddRange(values);

                newList.Add(innerList);
            }

            return newList;
        }
    }
}
