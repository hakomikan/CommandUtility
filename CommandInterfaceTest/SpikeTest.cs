using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Reflection;
using CommandInterface;
using static CommandInterfaceTest.CommandRunner;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace CommandInterfaceTest
{
    public class CommandRunner
    {
        public static void RunCommand(string commandName, params string[] parameters)
        {

        }
    }

    [TestClass]
    public class SpikeTest
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


    }
}
