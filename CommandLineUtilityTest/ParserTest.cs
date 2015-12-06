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
        class TestCommand
        {
            public static int Main(int numberArgument)
            {
                return 0;
            }
        }

        class TestCommand2
        {
            public static int Main(string stringArgument)
            {
                return 0;
            }
        }

        class TestCommand3
        {
            public static int Main(string stringArgument, int numberArgument)
            {
                return 0;
            }
        }

        class TestOneFlagCommand
        {
            public static int Main(bool flagArgument)
            {
                return 0;
            }
        }

        class TestOneKeywordCommand
        {
            public static int Main(string keywordArgument = "defaultValue")
            {
                return 0;
            }
        }

        class TestMixCommand
        {
            public static int Main(string stringArgument, int numberArgument, bool flagArgument, string keywordArgument = "defaultValue")
            {
                return 0;
            }
        }

        CommandArgumentsParser parser = new CommandArgumentsParser(typeof(TestCommand));
        CommandArgumentsParser parser2 = new CommandArgumentsParser(typeof(TestCommand2));
        CommandArgumentsParser parser3 = new CommandArgumentsParser(typeof(TestCommand3));
        CommandArgumentsParser flagArgumentParser = new CommandArgumentsParser(typeof(TestOneFlagCommand));
        CommandArgumentsParser keywordArgumentParser = new CommandArgumentsParser(typeof(TestOneKeywordCommand));
        CommandArgumentsParser mixParser = new CommandArgumentsParser(typeof(TestMixCommand));

        [TestMethod]
        public void TestFirstParser()
        {
            Assert.AreEqual(1234, (int)parser.ParseV("1234")[0]);
            Assert.AreEqual(2345, (int)parser.ParseV("2345")[0]);
            Assert.AreEqual("testString", (string)parser2.ParseV("testString")[0]);
            CollectionAssert.AreEqual(
                new List<object>() { "1234", 1234 },
                parser3.ParseV("1234", "1234"));
        }

        [TestMethod]
        public void TestSinglePositionalArgumentParser()
        {
            Assert.AreEqual(1234, (int)parser.Arguments[0].Parse("1234"));
            Assert.AreEqual("1234", (string)parser2.Arguments[0].Parse("1234"));
        }

        [TestMethod]
        public void TestArgumentParserType()
        {
            Assert.IsTrue(parser.Arguments[0].ArgumentInfo.IsPositionalArgument);
            Assert.IsFalse(flagArgumentParser.Arguments[0].ArgumentInfo.IsPositionalArgument);
        }

        [TestMethod]
        public void TestLackArgument()
        {
            AssertUtility.Throws<LackPositionalArgumentException>(() =>
            {
                parser3.ParseV("1234");
            });
        }

        [TestMethod]
        public void TestTooManyArgument()
        {
            AssertUtility.Throws<TooManyPositionalArgumentException>(() =>
            {
                parser.ParseV("1234", "1234");
            });
        }

        [TestMethod]
        public void TestInvalidTypeArgument()
        {
            AssertUtility.Throws<InvalidTypeArgumentException>(() =>
            {
                parser.ParseV("aaaa");
            });
        }

        [TestMethod]
        public void TestIdentifyArgumentType()
        {
            Assert.IsTrue(parser.IsPositionalArgumentValue("value"));
            Assert.IsFalse(parser.IsFlagArgumentValue("value"));
            Assert.IsTrue(flagArgumentParser.IsFlagArgumentValue("--flag-argument"));
            Assert.IsFalse(flagArgumentParser.IsPositionalArgumentValue("--flag-argument"));
            Assert.IsFalse(keywordArgumentParser.IsFlagArgumentValue("--keyword-argument"));
            Assert.IsFalse(keywordArgumentParser.IsPositionalArgumentValue("--keyword-argument"));
            Assert.IsTrue(keywordArgumentParser.IsKeywordArgumentValue("--keyword-argument"));
            Assert.AreEqual(
                CommandArgumentType.Positional,
                parser.IdentifyArgumentType("value"));
            Assert.AreEqual(
                CommandArgumentType.Flag,
                flagArgumentParser.IdentifyArgumentType("--flag-argument"));
            Assert.AreEqual(
                CommandArgumentType.Keyword,
                keywordArgumentParser.IdentifyArgumentType("--keyword-argument"));
        }

        [TestMethod]
        public void TestApplyArguments()
        {
            {
                bool found = false;
                Assert.IsTrue(
                    flagArgumentParser.ApplyFlagArgumentValue(new string[] { "--flag-argument" }, (arg) => { found = true; }).SequenceEqual(new List<string>()));
                Assert.IsTrue(found);
            }

            {
                Assert.IsTrue(
                    flagArgumentParser.ApplyFlagArgumentValue(new string[] { "restArgument" }, (arg) => {}).SequenceEqual(new List<string>() { "restArgument" }));
            }

            {
                bool found = false;
                Assert.IsTrue(
                    keywordArgumentParser.ApplyKeywordArgumentValue(new string[] { "--keyword-argument", "value" }, (arg, value) => { found = true; }).Count() == 0);
                Assert.IsTrue(found);
            }

            {
                AssertUtility.Throws<LackKeywordArgumentValueException>(() =>
                {
                    Assert.AreEqual(
                        0,
                        keywordArgumentParser.ApplyKeywordArgumentValue(new string[] { "--keyword-argument" }, (name, value) => { }).Count());
                });
            }

            {
                bool found = false;
                Assert.IsTrue(
                    parser.ApplyPositionalArgumentValue(new string[] { "value1", "value2" }, (arg, value) => { found = true; }).SequenceEqual(new string[] { "value2" }));
                Assert.IsTrue(found);
            }

            Assert.IsTrue(
                parser3.ApplyPositionalArgumentValue(new string[] { "value" }, (arg, value) => { }).SequenceEqual(new string[] { }));
        }

        [TestMethod]
        public void TestMatchArgument()
        {
            Assert.AreEqual(keywordArgumentParser.Arguments[0], keywordArgumentParser.MatchKeywordArgument("--keyword-argument"));
            Assert.AreEqual(null, keywordArgumentParser.MatchKeywordArgument("--undefined-argument"));
            Assert.AreEqual(flagArgumentParser.Arguments[0], flagArgumentParser.MatchFlagArgument("--flag-argument"));
            Assert.AreEqual(null, flagArgumentParser.MatchFlagArgument("--undefined-argument"));
        }

        [TestMethod]
        public void TestStoreArguments()
        {
            {
                Dictionary<SingleArgumentParser, string> argStore = parser.StoreRawArguments(new string[] { "1" });
                Assert.AreEqual("1", argStore[parser.Arguments[0]]);
            }
            {
                Dictionary<SingleArgumentParser, string> argStore = parser3.StoreRawArguments(new string[] { "1", "2" });
                Assert.AreEqual("1", argStore[parser3.Arguments[0]]);
                Assert.AreEqual("2", argStore[parser3.Arguments[1]]);
            }
            {
                Dictionary<SingleArgumentParser, string> argStore = mixParser.StoreRawArguments(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2"});
                Assert.AreEqual("str", argStore[mixParser.Arguments[0]]);
                Assert.AreEqual("2", argStore[mixParser.Arguments[1]]);
                Assert.AreEqual("", argStore[mixParser.Arguments[2]]);
                Assert.AreEqual("keywordValue", argStore[mixParser.Arguments[3]]);
            }
            {
                Dictionary<SingleArgumentParser, object> argStore = mixParser.StoreParsedArguments(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2" });
                Assert.AreEqual("str", argStore[mixParser.Arguments[0]]);
                Assert.AreEqual(2, argStore[mixParser.Arguments[1]]);
                Assert.AreEqual(true, argStore[mixParser.Arguments[2]]);
                Assert.AreEqual("keywordValue", argStore[mixParser.Arguments[3]]);
            }
        }

        [TestMethod]
        public void TestSequentialArguments()
        {
            {
                List<object> args = mixParser.ParseAsFunctionArguments(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2" });
                Assert.AreEqual("str", args[0]);
                Assert.AreEqual(2, args[1]);
                Assert.AreEqual(true, args[2]);
                Assert.AreEqual("keywordValue", args[3]);
            }
            {
                List<object> args = mixParser.ParseAsFunctionArguments(new string[] {"str", "2" });
                Assert.AreEqual("str", args[0]);
                Assert.AreEqual(2, args[1]);
                Assert.AreEqual(false, args[2]);
                Assert.AreEqual("defaultValue", args[3]);
            }
        }

        [TestMethod]
        [Ignore]
        public void TestFlagArgument()
        {
            CollectionAssert.AreEqual(
                new List<object>() { false },
                flagArgumentParser.ParseV());
            
            CollectionAssert.AreEqual(
                new List<object>() { true },
                flagArgumentParser.ParseV("--flag-argument"));
        }
    }
}
