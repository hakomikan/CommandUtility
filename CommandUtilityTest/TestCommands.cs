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

    class IntegerListCommand
    {
        public int Main(List<int> keywordValue)
        {
            return 0;
        }
    }

    class ParameterClass
    {
        public int numberArgument = 0;
        public string stringArgument = "defaultValue";
    }

    class ClassParameterCommand : CommandBase<ClassParameterCommand>
    {
        [CommandParameter]
        public int numberArgument = 0;

        [CommandParameter]
        public string stringArgument = "";

        [CommandParameter]
        public bool flagArgument = false;

        public int Main()
        {
            Console.WriteLine(numberArgument);
            return numberArgument * 2;
        }
    }

    class ParameterClassCommand
    {
        public int Main(ParameterClass parameterClass)
        {
            return 0;
        }
    }

    class TestParameters
    {
        public static CommandParameterInfo[] Parameters = new CommandClassInfo(typeof(TestMixCommand)).MainCommand.Parameters.ToArray();
        public static CommandParameterInfo[] Parameters2 = new CommandClassInfo(typeof(IntegerParamsCommand)).MainCommand.Parameters.ToArray();
        public static CommandParameterInfo[] Parameters3 = new CommandClassInfo(typeof(IntegerListCommand)).MainCommand.Parameters.ToArray();
        public static CommandParameterInfo StringPositionalArgument = Parameters[0];
        public static CommandParameterInfo NumberPositionalArgument = Parameters[1];
        public static CommandParameterInfo FlagArgument = Parameters[2];
        public static CommandParameterInfo KeywordArgument = Parameters[3];
        public static CommandParameterInfo IntegerParamsArgument = Parameters2[0];
        public static CommandParameterInfo IntegerListArgument = Parameters3[0];
    }
}
