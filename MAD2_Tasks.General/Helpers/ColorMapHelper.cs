using System.Collections.Generic;

namespace MAD2_Tasks.General.Helpers
{
    public static class ColorMapHelper
    {
        public static Dictionary<int, string> GetColorMap() => new Dictionary<int, string>
                                                               {
                                                                   { 0, "#1B60C1" }, //blue
                                                                   { 1, "#1BC156" }, //green
                                                                   { 2, "#DD1717" }, //red
                                                                   { 3, "#DD9217" }, //orange
                                                                   { 4, "#BD30DF" }, //purple
                                                                   { 5, "#ECE13B" }, //yellow
                                                                   { 6, "#00B8C6" }, //light blue
                                                                   { 7, "#1B2659" }, //dark blue
                                                                   { 8, "#1B5932" }, //dark green
                                                                   { 9, "#59241B" }, //dark red
                                                                   { 10, "#84FF3C " }, //light green
                                                               };
    }
}
