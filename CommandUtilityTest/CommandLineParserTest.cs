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
        public void TestClassifyArgument()
        {
            var parser = new CommandLineParser(typeof(TestMixCommand));

            var classified = parser.ClassifyArgument("--keyword-argument");
            Assert.AreEqual(ArgumentType.Keyword, classified.ArgumentType);
            Assert.AreEqual("keywordArgument", classified.ParameterInfo.ParameterInfo.Name);
        }

        [TestMethod]
        [Ignore]
        public void TestCommandLineParser()
        {
            var parser = new CommandLineParser(typeof(TestCommand));

            var argumentStore = parser.ParseV("1");

            CollectionAssert.AreEqual(
                new object[] {1}, argumentStore.FunctionArguments);
        }
    }

    public enum ArgumentType
    {
        Flag,
        Keyword,
        Value
    }

    public class ClassifiedArgument
    {
        public ArgumentType ArgumentType { get; set; }
        public CommandParameterInfo ParameterInfo { get; set; }
        public string Value { get; set; }
    }

    public class CommandLineParser
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

            var restArguments1 = ParseKeywordArguments(result, arguments);
            var restArguments2 = ParseFlagArguments(result, restArguments1);
            var restArguments3 = ParseSequentialArguments(result, restArguments2);

            result.RestArguments = restArguments3.ToArray();

            return result;
        }

        private IEnumerable<string> ParseKeywordArguments(WholeArgumentStore result, IEnumerable<string> arguments)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
#if false
                var current = enumerator.Current;

                var argumentParser = MatchKeywordArgument(current);
                if (argumentParser != null)
                {
                    if (enumerator.MoveNext())
                    {
                        var value = enumerator.Current;
                        if(IsValue(value))
                        {
                            result.StoreValue(argumentParser, value);
                        }
                        else
                        {
                            throw new LackKeywordArgumentValueException("lack value after: " + current);
                        }
                    }
                    else
                    {
                        throw new LackKeywordArgumentValueException("lack value after: " + current);
                    }
                }
                else
                {
                    yield return current;
                }
#endif
            }
            throw new NotImplementedException();
        }

        private IEnumerable<string> ParseFlagArguments(WholeArgumentStore result, IEnumerable<string> restArguments1)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> ParseSequentialArguments(WholeArgumentStore result, IEnumerable<string> restArguments2)
        {
            throw new NotImplementedException();
        }

        public ClassifiedArgument ClassifyArgument(string v)
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
