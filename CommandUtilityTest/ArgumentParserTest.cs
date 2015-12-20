using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandUtilityTest
{
    [TestClass]
    public class ArgumentParserTest
    {
        ParameterInfo[] methods = new CommandClassInfo(typeof(TestMixCommand)).MainCommand.GetParameters();

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
