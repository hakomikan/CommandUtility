using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Reflection;
using CommandInterface;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace CommandInterfaceTest
{
    [TestClass]
    public class SpikeTest : CommandInterfaceTestBase
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            Assert.AreEqual(await CSharpScript.EvaluateAsync<int>("1 + 2"), 3);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            RunCommand("create", "new-command");
            RunCommand("edit", "new-command");
            RunCommand("list", "new-command");
            RunCommand("delete", "new-command");
        }

        public int RunCommand(string commandName, params string[] parameters)
        {
            return CommandManager.Execute(commandName, parameters);
        }

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
        }

        [TestMethod]
        public void SubCommandTest()
        {
            Assert.AreEqual(333, RunCommand("test-script"));
            //Assert.AreEqual(669, RunCommand("test-script2", "3"));
            new CommandUtility.CommandInterface(typeof(TestMixCommand)).Run(new string[] { "--flag-argument", "--keyword-argument", "keywordValue", "str" });
        }

    }
}
