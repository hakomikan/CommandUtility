using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface;

namespace CommandInterfaceTest
{
    [TestClass]
    public class CommandManagerTest : CommandInterfaceTestBase
    {
        [TestMethod]
        public void ListCommands()
        {
            Console.WriteLine(string.Join(",", CommandManager.ListCommands()));

            var commands = CommandManager.ListCommands();
            Assert.IsTrue(2 <= commands.Length);
            CollectionAssert.Contains(commands, "test-script");
            CollectionAssert.Contains(commands, "test-script2");
        }

        [TestMethod]
        public void GetFullPathFromCommandName()
        {
            Assert.AreEqual(
                GetTestScriptPath("TestScript.cs"),
                CommandManager.GetScriptPath("test-script"));
        }

        [TestMethod]
        public void ExecuteScript()
        {
            Assert.AreEqual(333, CommandManager.Execute("test-script"));
            Assert.AreEqual(666, CommandManager.Execute("test-script2"));
            Assert.AreEqual(444, CommandManager.Execute("test-script-use-module"));
        }
    }
}
