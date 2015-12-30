using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class CommandLineParserTest
    {
        [TestMethod]
        public void TestCommandLineParser()
        {
            var parser = new CommandLineParser(typeof(TestCommand));

            var argumentStore = parser.ParseV("1");

            CollectionAssert.AreEqual(
                new object[] {1}, argumentStore.FunctionArguments);
        }
    }

    internal class CommandLineParser
    {
        private CommandClassInfo CommandClassInfo;

        public CommandLineParser(Type type)
        {
            this.CommandClassInfo = new CommandClassInfo(type);
        }

        public WholeArgumentStore ParseV(params string[] arguments)
        {
            return Parse(arguments);
        }

        public WholeArgumentStore Parse(string[] arguments)
        {
            var result = new WholeArgumentStore();

            var restArguments1 = ParseKeywordArguments(ref result, arguments);
            var restArguments2 = ParseFlagArguments(ref result, restArguments1);
            var restArguments3 = ParseSequentialArguments(ref result, restArguments2);

            result.RestArguments = restArguments3.ToArray();

            return result;
        }

        private IEnumerable<string> ParseSequentialArguments(ref WholeArgumentStore result, IEnumerable<string> restArguments2)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> ParseFlagArguments(ref WholeArgumentStore result, IEnumerable<string> restArguments1)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> ParseKeywordArguments(ref WholeArgumentStore result, string[] arguments)
        {
            throw new NotImplementedException();
        }
    }

    public class WholeArgumentStore
    {
        public Dictionary<CommandParameterInfo, IArgumentStore> ArgumentStores = new Dictionary<CommandParameterInfo, IArgumentStore>();
        public object[] FunctionArguments { get; private set; }
        public string[] RestArguments { get; set; }
    }
}
