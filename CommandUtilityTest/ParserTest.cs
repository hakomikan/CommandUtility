using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Collections.Generic;
using System.Linq;

namespace CommandUtilityTest
{
    [TestClass]
    public class ParserTest
    {
        CommandLineParser parser = new CommandLineParser(typeof(TestCommand));
        CommandLineParser parser2 = new CommandLineParser(typeof(TestCommand2));
        CommandLineParser parser3 = new CommandLineParser(typeof(TestCommand3));
        CommandLineParser flagArgumentParser = new CommandLineParser(typeof(TestOneFlagCommand));
        CommandLineParser keywordArgumentParser = new CommandLineParser(typeof(TestOneKeywordCommand));
        CommandLineParser mixParser = new CommandLineParser(typeof(TestMixCommand));

        [TestMethod]
        public void TestLackArgument()
        {
            AssertUtility.Throws<LackArgumentException>(() =>
            {
                parser3.ParseAsFunctionArguments(new string[] { "1234" });
            });
        }

        [TestMethod]
        public void TestTooManyArgument()
        {
            AssertUtility.Throws<TooManyArgumentException>(() =>
            {
                parser.ParseAsFunctionArguments(new string[] { "1234", "1234" });
            });
        }

        [TestMethod]
        public void TestInvalidTypeArgument()
        {
            AssertUtility.Throws<InvalidTypeArgumentException>(() =>
            {
                parser.ParseAsFunctionArguments(new string[] { "aaaa" });
            });
        }

        [TestMethod]
        public void TestSequentialArguments()
        {
            {
                var args = mixParser.ParseAsFunctionArguments(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2" });
                Assert.AreEqual("str", args[0]);
                Assert.AreEqual(2, args[1]);
                Assert.AreEqual(true, args[2]);
                Assert.AreEqual("keywordValue", args[3]);
            }
            {
                var args = mixParser.ParseAsFunctionArguments(new string[] {"str", "2" });
                Assert.AreEqual("str", args[0]);
                Assert.AreEqual(2, args[1]);
                Assert.AreEqual(false, args[2]);
                Assert.AreEqual("defaultValue", args[3]);
            }
        }
    }
}
