using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandUtilityTest
{
    [TestClass]
    public class ArgumentParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var argumentParser = new ArgumentParser(TestParameters.NumberPositionalArgument);
            //var valueStore = argumentParser.CreateStore();

            var valueStore = new ArgumentValueStore<int>();
            valueStore.Store(1);
        }
    }

    public class ArgumentValueStore<T>
    {
        public ArgumentValueStore()
        {
        }

        public void Store(int v)
        {
            
        }
    }

    public class ArgumentParser
    {
        private ParameterInfo ParameterInfo;

        public ArgumentParser(ParameterInfo parameterInfo)
        {
            this.ParameterInfo = parameterInfo;
        }
    }
}
