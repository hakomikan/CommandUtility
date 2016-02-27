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
            var instance = default(T);

            var instanceArgumentCollector = new InstanceArgumentCollector<T>();
            var instanceAssigner = new InstanceAssigner<T>(instance);
            instanceAssigner.AssginArguments(instanceArgumentCollector);

            var functionArgumentCollector = new FunctionArgumentCollector<T>();
            var invoker = new CommandUtility.CommandInvoker<T>(instance);
            return invoker.Invoke(functionArgumentCollector.GetArguments());
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
