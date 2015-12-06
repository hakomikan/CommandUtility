using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility.Expressions
{
    public class Hex32Attribute : ICommandArgumentConverterAttribute
    {
        public override Type OutputType { get { return typeof(UInt32); } }

        public override object Convert(string argument)
        {
            if (argument.StartsWith("0x"))
            {
                try
                {
                    return UInt32.Parse(argument.Substring(2), System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    throw new InvalidTypeArgumentException(string.Format("Invalid expression: expected=0x1234ABCD, actual={0}", argument));
                }
            }
            else
            {
                throw new InvalidTypeArgumentException(string.Format("Invalid expression: expected=0x1234ABCD, actual={0}", argument));
            }
        }
    }
}
