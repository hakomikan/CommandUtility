using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CommandInterface.Utility.NameConverter;

namespace CommandInterfaceTest
{
    [TestClass]
    public class NameConverterTest
    {
        [TestMethod]
        public void TestFileNameToCommandName()
        {
            Assert.AreEqual("test-script", ConvertToCommandName("TestScript.cs"));
            Assert.AreEqual("test-script2", ConvertToCommandName("TestScript2.cs"));
        }

        [TestMethod]
        public void TestCommandNameToFileName()
        {
            Assert.AreEqual("TestScript.cs", ConvertToFileName("test-script"));
            Assert.AreEqual("TestScript2.cs", ConvertToFileName("test-script2"));
        }
    }
}
