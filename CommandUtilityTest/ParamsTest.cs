using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Collections.Generic;

namespace CommandUtilityTest
{
    [TestClass]
    public class ParamsTest
    {
        [Export(typeof(ICommandArgumentConverter))]
        class StringArrayConverter : ICommandArgumentConverter
        {
            public Type OutputType { get { return typeof(string[]); } }

            public object Convert(string argument)
            {
                return new string[] { argument };
            }
        }

        [TestMethod]
        public void TestParamsCommand()
        {
            var command = new CommandInterface<StringParamsCommand>();

            CollectionAssert.AreEqual(
                new string[] { "a" },
                (string[])(command.Parser.ParseV("a").FunctionArguments[0]));

            CollectionAssert.AreEqual(
                new string[] { "a", "b" },
                (string[])command.Parser.ParseV("a", "b").FunctionArguments[0]);

        }
    }
}
