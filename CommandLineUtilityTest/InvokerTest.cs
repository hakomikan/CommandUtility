using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class InvokerTest
    {
        class TestCommand
        {
            public string Parameter { get; set; }

            public int Main(string stringArgument)
            {
                Parameter = stringArgument;
                return 123;
            }
        }

        [TestMethod]
        public void TestInvokeCommand()
        {
            var invoker = new CommandInvoker<TestCommand>();
            Assert.AreEqual(123, invoker.Invoke(new object[] { "argument" }));
            Assert.AreEqual("argument", invoker.Instance.Parameter);
        }
    }
}
