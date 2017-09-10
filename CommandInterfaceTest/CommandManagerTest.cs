using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface;

namespace CommandInterfaceTest
{
    [TestClass]
    public class CommandManagerTest
    {
        public CommandManagerTest()
        {
            this.CommandManager = new CommandManager(GetTestConfig());
        }

        public CommandManager CommandManager { get; set; }

        public DirectoryInfo GetScriptDirectory()
        {
            return new DirectoryInfo(
                Path.GetFullPath(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "../../Scripts")));
        }

        public string GetTestScriptPath(string filename)
        {
            return Path.Combine(GetScriptDirectory().FullName, filename);
        }

        public Config GetTestConfig()
        {
            return new Config()
            {
                ScriptDirectory = GetScriptDirectory()
            };
        }

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
