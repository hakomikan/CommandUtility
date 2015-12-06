using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using CommandUtility.Expressions;
using System.Linq;

namespace CommandUtilityTest
{
    [TestClass]
    public class InterfaceTest
    {
        class TestMixCommand : CommandBase<TestMixCommand>
        {
            public static string StringArgument { get; set; }
            public static int NumberArgument { get; set; }
            public static bool FlagArgument { get; set; }
            public static string KeywordArgument { get; set; }
            public static UInt32 HexArgument { get; set; }

            public int Main(string stringArgument, int numberArgument, bool flagArgument, string keywordArgument = "defaultValue", [Hex32] UInt32 hexArgument = 0U)
            {
                StringArgument = stringArgument;
                NumberArgument = numberArgument;
                FlagArgument = flagArgument;
                KeywordArgument = keywordArgument;
                HexArgument = hexArgument;

                return 0;
            }
        }

        [TestMethod]
        public void IntafaceTest()
        {
            var command = new CommandInterface<TestMixCommand>();

            Assert.AreEqual(0, command.Run(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str", "2" }));
            Assert.AreEqual("str", TestMixCommand.StringArgument);
            Assert.AreEqual(2, TestMixCommand.NumberArgument);
            Assert.AreEqual(true, TestMixCommand.FlagArgument);
            Assert.AreEqual("keywordValue", TestMixCommand.KeywordArgument);
            Assert.AreEqual(0U, TestMixCommand.HexArgument);

            Assert.AreEqual(0, command.Run(new string[] { "str2", "22", "--hex-argument", "0x1234DCBA" }));
            Assert.AreEqual("str2", TestMixCommand.StringArgument);
            Assert.AreEqual(22, TestMixCommand.NumberArgument);
            Assert.AreEqual(false, TestMixCommand.FlagArgument);
            Assert.AreEqual("defaultValue", TestMixCommand.KeywordArgument);
            Assert.AreEqual(0x1234DCBAU, TestMixCommand.HexArgument);

            TestMixCommand.Main(new string[] { "commandName", "--keyword-argument", "keywordValue2", "str3", "23" });
            Assert.AreEqual("str3", TestMixCommand.StringArgument);
            Assert.AreEqual(23, TestMixCommand.NumberArgument);
            Assert.AreEqual(false, TestMixCommand.FlagArgument);
            Assert.AreEqual("keywordValue2", TestMixCommand.KeywordArgument);
        }
    }
}
