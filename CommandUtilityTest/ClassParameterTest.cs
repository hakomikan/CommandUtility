using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Linq;

namespace CommandUtilityTest
{
    [TestClass]
    public class ClassParameterTest
    {
        [TestMethod]
        public void TestClassParameter()
        {
            var info = new CommandClassInfo(typeof(ClassParameterCommand));
            Assert.AreEqual(1, info.Parameters.Count());
        }

        [TestMethod]
        public void TestClassParameterType()
        {
            var info = new CommandClassInfo(typeof(ClassParameterCommand));

            Assert.AreEqual(CommandArgumentType.Keyword, info.Parameters.ToArray()[0].ArgumentType);
            Assert.AreEqual(CommandArgumentType.Keyword, info.Parameters.ToArray()[1].ArgumentType);
            Assert.AreEqual(CommandArgumentType.Flag,    info.Parameters.ToArray()[2].ArgumentType);
        }

        [TestMethod]
        public void TestParseClassParameter()
        {
            var parser = new CommandLineParser(typeof(ClassParameterCommand));

            var store = parser.ParseV("3");
            Assert.AreEqual(0, store.RestArguments.Length);
        }

        [TestMethod]
        public void TestInvokeClassParameterCommandAsCommandInterface()
        {
            var command = new CommandInterface<ClassParameterCommand>();
            Assert.AreEqual(6, command.Run(new string[] { "3" }));
        }

        [TestMethod]
        public void TestInvokeClassParameterCommand()
        {
            var command = new ClassParameterCommand();
            Assert.AreEqual(6, command.Invoke(new string[] { "3" }));
            Assert.AreEqual(3, command.numberArgument);
        }
    }
}
