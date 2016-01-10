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
        [Ignore]
        public void TestClassParameter()
        {
            var info = new CommandClassInfo(typeof(ClassParameterCommand));

            Assert.AreEqual(1, info.MainCommand.Parameters.Count());
        }
    }
}
