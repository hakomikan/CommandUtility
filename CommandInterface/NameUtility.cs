using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CommandInterface
{
    public static class NameUtility
    {
        private static readonly Regex ScriptNamePattern = new Regex("^([A-Z][a-z0-9]*)+.cs$");

        public static string ConvertToCommandName(string fileName)
        {
            var matched = ScriptNamePattern.Match(fileName);

            if (!matched.Success)
            {
                throw new InvalidDataException();
            }

            var ret = new List<string>();

            foreach (Capture capture in matched.Groups[1].Captures)
            {
                ret.Add(capture.Value.ToLower());
            }

            return string.Join("-", ret);
        }
    }
}
