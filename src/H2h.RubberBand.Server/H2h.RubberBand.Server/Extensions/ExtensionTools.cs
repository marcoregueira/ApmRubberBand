using System.Collections.Generic;
using System.IO;

namespace H2h.RubberBand.Server.Extensions
{
    public static class ExtensionTools
    {
        internal static IEnumerable<string> ReadLines(this string s)
        {
            string line;
            using (var sr = new StringReader(s))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }
    }
}