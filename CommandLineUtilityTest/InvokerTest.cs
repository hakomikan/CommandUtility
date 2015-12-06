using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandLineUtilityTest
{
    [TestClass]
    public class InvokerTest
    {
        [TestMethod]
        public void TestInvokeCommand()
        {
            var invoker = new CommandInvoker(typeof(TestCommand));
            invoker.Invoke(new object[] { "argument" });
        }
    }
}
