using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace CommandUtility.Converters
{
    [Export(typeof(ICommandArgumentConverter))]
    class StringConverter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(string); } }

        public object Convert(string argument)
        {
            return argument;
        }
    }

    [Export(typeof(ICommandArgumentConverter))]
    class IntConverter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(int); } }

        public object Convert(string argument)
        {
            return int.Parse(argument);
        }
    }

    [Export(typeof(ICommandArgumentConverter))]
    class FloatConverter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(float); } }

        public object Convert(string argument)
        {
            return float.Parse(argument);
        }
    }

    [Export(typeof(ICommandArgumentConverter))]
    class DoubleConverter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(double); } }

        public object Convert(string argument)
        {
            return double.Parse(argument);
        }
    }

    [Export(typeof(ICommandArgumentConverter))]
    class Int32Converter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(Int32); } }

        public object Convert(string argument)
        {
            return Int32.Parse(argument);
        }
    }
    [Export(typeof(ICommandArgumentConverter))]
    class Int64Converter : ICommandArgumentConverter
    {
        public Type OutputType { get { return typeof(Int64); } }

        public object Convert(string argument)
        {
            return Int64.Parse(argument);
        }
    }
}
