using System;

namespace CommandUtility.Expressions
{
    public class Hex64Attribute : ICommandArgumentConverterAttribute
    {
        public override Type OutputType { get { return typeof(UInt64); } }

        public override object Convert(string argument)
        {
            if (argument.StartsWith("0x"))
            {
                try
                {
                    return UInt64.Parse(argument.Substring(2), System.Globalization.NumberStyles.HexNumber);
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