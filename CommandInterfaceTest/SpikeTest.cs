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

        [TestMethod]
        public void SubCommandTest()
        {
            Assert.AreEqual(333, RunCommand("test-script"));
            Assert.AreEqual(666, RunCommand("test-script2", "3"));
        }

    }
}
