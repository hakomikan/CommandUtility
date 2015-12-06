using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class ConverterTest
    {
        class TestFloatCommand
        {
            public int Main(float floatArgument) { return 0; }
        }

        class TestInt64Command
        {
            public int Main(Int64 int64Argument) { return 0; }
        }

        CommandArgumentsParser floatParser = new CommandArgumentsParser(typeof(TestFloatCommand));
        CommandArgumentsParser int64Parser = new CommandArgumentsParser(typeof(TestInt64Command));

        [TestMethod]
        public void TestArgumentConverter()
        {
            var converter = new CommandArgumentConverter();
            Assert.AreEqual(0.1f, converter.Convert(typeof(float), "0.1"));
            Assert.AreEqual(1L, converter.Convert(typeof(Int64), "1"));
            Assert.AreEqual(0.2, converter.Convert(typeof(double), "0.2"));
        }

        [TestMethod]
        public void TestParsingManyTypes()
        {
            Assert.AreEqual(0.1f, floatParser.Arguments[0].Parse("0.1"));
            Assert.AreEqual(1L, int64Parser.Arguments[0].Parse("1"));
        }
    }
}
