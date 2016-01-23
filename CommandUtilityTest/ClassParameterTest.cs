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
        public void TestInvokeClassParameterCommandAsCommandInterface()
        {
            var command = new CommandInterface<ClassParameterCommand>();
            Assert.AreEqual(6, command.Run(new string[] { "3" }));
        }

        [TestMethod]
        public void TestInvokeClassParameterCommand()
        {
            var command = new ClassParameterCommand();
            Assert.AreEqual(123, command.Invoke(new string[] { "3" }));
            Assert.AreEqual(3, command.numberArgument);
        }
    }
}
