using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Linq;

namespace CommandLineUtilityTest
{
    [TestClass]
    public class InterfaceTest
    {
        class TestMixCommand
        {
            public static string StringArgument { get; set; }
            public static int NumberArgument { get; set; }
            public static bool FlagArgument { get; set; }
            public static string KeywordArgument { get; set; }

            public int Main(string stringArgument, int numberArgument, bool flagArgument, string keywordArgument = "defaultValue")
            {
                StringArgument = stringArgument;
                NumberArgument = numberArgument;
                FlagArgument = flagArgument;
                KeywordArgument = keywordArgument;

                return 0;
            }

            public static void Main(string[] args)
            {
                new CommandInterface<TestMixCommand>().Run(args.Skip(1).ToArray());
            }
        }

        [TestMethod]
        public void IntafaceTest()
        {
            var command = new CommandInterface<TestMixCommand>();
            var instance = command.Invoker.Instance;

            Assert.AreEqual(0, command.Run(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2" }));
            Assert.AreEqual("str", TestMixCommand.StringArgument);
            Assert.AreEqual(2, TestMixCommand.NumberArgument);
            Assert.AreEqual(true, TestMixCommand.FlagArgument);
            Assert.AreEqual("keywordValue", TestMixCommand.KeywordArgument);

            Assert.AreEqual(0, command.Run(new string[] { "str2", "22" }));
            Assert.AreEqual("str2", TestMixCommand.StringArgument);
            Assert.AreEqual(22, TestMixCommand.NumberArgument);
            Assert.AreEqual(false, TestMixCommand.FlagArgument);
            Assert.AreEqual("defaultValue", TestMixCommand.KeywordArgument);

            TestMixCommand.Main(new string[] { "commandName", "--keyword-argument", "keywordValue2", "str3", "23" });
            Assert.AreEqual("str3", TestMixCommand.StringArgument);
            Assert.AreEqual(23, TestMixCommand.NumberArgument);
            Assert.AreEqual(false, TestMixCommand.FlagArgument);
            Assert.AreEqual("keywordValue2", TestMixCommand.KeywordArgument);
        }
    }
}
