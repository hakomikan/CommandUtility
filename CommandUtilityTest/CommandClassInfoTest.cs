using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class CommandClassInfoTest
    {
        [TestMethod]
        public void TestCommandClassInfo()
        {
            var commandInfo = new CommandClassInfo(typeof(TestMixCommand));
            var commandNoInfo = new CommandClassInfo(typeof(TestNoCommand));

            Assert.IsTrue(commandInfo.HasMainCommand);
            Assert.IsFalse(commandNoInfo.HasMainCommand);
        }

        [TestMethod]
        public void TestMultiValuedParameter()
        {
            Assert.IsFalse(TestParameters.NumberPositionalArgument.IsMultiValued);
            Assert.IsTrue(TestParameters.IntegerParamsArgument.IsMultiValued);
            Assert.IsTrue(TestParameters.IntegerListArgument.IsMultiValued);
            Assert.AreEqual(typeof(int), TestParameters.NumberPositionalArgument.ParameterType);
            Assert.AreEqual(typeof(int), TestParameters.IntegerParamsArgument.ParameterType);
            Assert.AreEqual(typeof(int), TestParameters.IntegerListArgument.ParameterType);
        }

        class TestClass
        {
            public int IntField = 3;
        }

        [TestMethod]
        public void TestClassParameterInfo()
        {
            var info = new CommandClassInfo(typeof(ClassParameterCommand));

            Assert.AreEqual(1, info.Parameters.Count());
            Assert.AreEqual("numberArgument", info.Parameters.First().Name);
        }
    }

}
