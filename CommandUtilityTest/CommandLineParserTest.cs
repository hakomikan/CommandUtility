using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Text.RegularExpressions;

namespace CommandUtilityTest
{
    [TestClass]
    public class CommandLineParserTest
    {
        [TestMethod]
        public void TestClassifyArgument()
        {
            var parser = new CommandLineParser(typeof(TestMixCommand));

            {
                var classified = parser.ClassifyArgument("--keyword-argument");
                Assert.AreEqual(CommandLineParser.ArgumentType.Keyword, classified.ArgumentType);
                Assert.AreEqual("keywordArgument", classified.ParameterInfo.ParameterInfo.Name);
            }

            {
                var classified = parser.ClassifyArgument("--flag-argument");
                Assert.AreEqual(CommandLineParser.ArgumentType.Flag, classified.ArgumentType);
                Assert.AreEqual("flagArgument", classified.ParameterInfo.ParameterInfo.Name);
            }

            {
                var classified = parser.ClassifyArgument("stringValue");
                Assert.AreEqual(CommandLineParser.ArgumentType.Value, classified.ArgumentType);
                Assert.AreEqual("stringValue", classified.Value);
            }

            AssertUtility.Throws<UnknownOptionException>(() => {
                var classified = parser.ClassifyArgument("--unknown-option");
            });
        }

        [TestMethod]
        public void TestCommandLineParser()
        {
            CollectionAssert.AreEqual(
                new object[] {"1", 3, true, "2"},
                new CommandLineParser(typeof(TestMixCommand))
                    .ParseV("--flag-argument", "1", "--keyword-argument", "2", "3")
                    .FunctionArguments);

            CollectionAssert.AreEqual(
                new int[] { 1, 2, 3, 4 },
                (int[])new CommandLineParser(typeof(IntegerParamsCommand))
                    .ParseV("1", "2", "3", "4")
                    .FunctionArguments[0]);
        }
    }




}
