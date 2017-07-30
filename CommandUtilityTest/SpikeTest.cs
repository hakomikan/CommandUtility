using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandUtilityTest
{
    [TestClass]
    public class SpikeTest
    {
        MixParameterCommand TargetInstance = new MixParameterCommand();
        CommandInterface<MixParameterCommand> TargetCommand = new CommandInterface<MixParameterCommand>();

        [TestMethod]
        [Ignore]
        public void TestCommandInterface()
        {
            var command = new CommandInterface<MixParameterCommand>();

            Assert.AreEqual(6, command.RunV("3", "4", "--number-argument1", "2", "--string-argument1", "aaaa"));
        }

        [TestMethod]
        [Ignore]
        public void TestGetParameters()
        {
            var parameters = TargetCommand.Collect(TargetInstance);
            Assert.AreEqual(4, parameters.Count);
        }

        [TestMethod]
        [Ignore]
        public void TestGetClassMemberParameters()
        {
            var classMemberParamters = TargetCommand.ClassMemberParamter;
            Assert.AreEqual(2, classMemberParamters.Count);
        }
    }

    public interface ICommandParameter
    {
    }

    class CommandInterface<T> where T : class, new()
    {
        public List<ICommandParameter> ClassMemberParamter
        {
            get
            {
                typeof(T).GetFields();
                return null;
            }
        }
        public List<ICommandParameter> MainCommandParameter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
#if false
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
#else
            throw new NotImplementedException();
#endif
        }

        public List<ICommandParameter> Collect(T instance)
        {
            // collect ClassMemberParameter
            var classMemberParamter = this.ClassMemberParamter;
            // collect MainCommandParameter
            var mainCommandParameter = this.MainCommandParameter;

            return classMemberParamter.Concat(mainCommandParameter).ToList();
        }
    }
}
