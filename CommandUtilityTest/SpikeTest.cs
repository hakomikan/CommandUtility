using System;
using System.Collections.Generic;
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
            var instance = default(T);
            return Run(instance, arguments);
        }

        public int Run(T instance, string[] arguments)
        {
            // CollectParameters
            List<ICommandParameter> parameters = this.Collect(instance);
            
            // Match
            List<Tuple<ICommandParameter, string>> machedArguments = this.Match(parameters, arguments);
            
            // Convert
            List<Tuple<ICommandParameter, object>> convertedArguments = this.Convert(machedArguments);

            // Assign
            this.Assign(instance, convertedArguments);

            // Invoke
            return this.Invoke(instance, convertedArguments);
        }
    }
}
