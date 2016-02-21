using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandUtilityTest
{
    [TestClass]
    public class SpikeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var command = new CommandInterface<MixParameterCommand>();

            Assert.AreEqual(6, command.RunV("3", "4", "--number-argument1", "2", "--string-argument1", "aaaa"));
        }
    }

    class CommandInterface<T> where T : class, new()
    {
        public int RunV(params string[] arguments)
        {
            return Run(arguments);
        }

        public int Run(string[] arguments)
        {
            // Convert
            // Match
            // Process
            //   Assign
            //   Invoke
            var functionArgumentCollector = new FunctionArgumentCollector<T>();
            var functionArguments = functionArgumentCollector.GetArguments();
            var invoker = new CommandUtility.CommandInvoker<T>();
            return invoker.Invoke(functionArguments);
        }
    }

    class FunctionArgumentCollector<T> where T : class, new()
    {
        public FunctionArgumentCollector()
        {
        }

        public object[] GetArguments()
        {
            throw new NotImplementedException();
        }
    }
}
