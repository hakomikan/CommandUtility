using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandUtility;
using System.Reflection;

namespace CommandUtilityTest
{
    class TestNoCommand
    {
    }

    class TestCommand
    {
        public int Main(int numberArgument)
        {
            return 0;
        }
    }

    class TestCommand2
    {
        public int Main(string stringArgument)
        {
            return 0;
        }
    }

    class TestCommand3
    {
        public int Main(string stringArgument, int numberArgument)
        {
            return 0;
        }
    }

    class TestOneFlagCommand
    {
        public int Main(bool flagArgument)
        {
            return 0;
        }
    }

    class TestOneKeywordCommand
    {
        public int Main(string keywordArgument = "defaultValue")
        {
            return 0;
        }
    }

    class TestMixCommand
    {
        public int Main(string stringArgument, int numberArgument, bool flagArgument, string keywordArgument = "defaultValue")
        {
            return 0;
        }
    }

    class IntegerParamsCommand
    {
        public int Main(params int[] arguments)
        {
            return 0;
        }
    }

    class StringParamsCommand
    {
        public int Main(params string[] arguments)
        {
            return 0;
        }
    }

    class TestParameters
    {
        public static ParameterInfo[] Parameters = new CommandClassInfo(typeof(TestMixCommand)).MainCommand.GetParameters();
        public static ParameterInfo[] Parameters2 = new CommandClassInfo(typeof(IntegerParamsCommand)).MainCommand.GetParameters();
        public static ParameterInfo StringPositionalArgument = Parameters[0];
        public static ParameterInfo NumberPositionalArgument = Parameters[1];
        public static ParameterInfo FlagArgument = Parameters[2];
        public static ParameterInfo KeywordArgument = Parameters[3];
        public static ParameterInfo IntegerParamsArgument = Parameters2[0];
    }
}
