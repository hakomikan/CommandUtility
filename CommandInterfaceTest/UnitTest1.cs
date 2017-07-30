﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Reflection;
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
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            Assert.AreEqual(await CSharpScript.EvaluateAsync<int>("1 + 2"), 3);
        }

        [TestMethod]
        public void RunAsScript()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"using System;
using System.Collections.Generic;

namespace CommandInterfaceTest
{
    public class TestScript
    {
        public static int Main()
        {
            return 0;
        }
    }
}
");

            CSharpCompilation compilation = CSharpCompilation.Create(
                "ScriptAssembly",
                new[] { syntaxTree },
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var dll = new MemoryStream())
            using (var pdb = new MemoryStream())
            {
                var emitResult = compilation.Emit(dll, pdb);
                if (emitResult.Success)
                {
                    var assembly = Assembly.Load(dll.ToArray(), pdb.ToArray());
                    var types = assembly.GetTypes();
                    var targetClass = types[0];
                    var targetMethods = targetClass.GetMethods();
                    var methodBase = targetClass.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);

                    Assert.AreEqual(0, methodBase.Invoke(null, null));
                }
                else
                {
                    throw new Exception("ahhh");
                }
            }
        }

        [TestMethod]
        public void InterfaceTest()
        {
            RunCommand("create", "new-command");
            RunCommand("edit", "new-command");
            RunCommand("list", "new-command");
            RunCommand("delete", "new-command");
        }

        public DirectoryInfo GetScriptDirectory()
        {
            return new DirectoryInfo(
                Path.GetFullPath(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "../../Scripts")));
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
            var commandManager = new CommandManager(GetTestConfig());

            Console.WriteLine(GetScriptDirectory().FullName);
            Console.WriteLine(string.Join(",", commandManager.ListCommands()));

            CollectionAssert.AreEqual(
                new string[]
                {
                    "test-script-1",
                    "test-script-2"
                },
                commandManager.ListCommands());
        }
    }
}