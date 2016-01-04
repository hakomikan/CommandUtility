using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandUtility
{
    public class CommandExpressionUtility
    {
        public static bool IsOptionExpression(string argument)
        {
            return Regex.IsMatch(argument, "(-[-_A-Za-z0-9])|(--[-_A-Za-z0-9]+)");
        }
    }
}
