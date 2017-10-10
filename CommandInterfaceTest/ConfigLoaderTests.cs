using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface.Tests
{
    [TestClass()]
    public class ConfigLoaderTests
    {
        public static string TestConfig = @"
namespace CommandInterface.Tests
{
    class TestCommandSetConfig : CommandSetConfig
    {
        public TestCommandSetConfig()
        {
            this.Name = ""TestCommandSet"";
            this.ScriptDirectory = ""TestDirectory"";
            this.ProjectDirecotry = ""TestProjectDirectory"";
        }
    }
}
";
        [TestMethod()]
        public void LoadTest()
        {
            var loader = new ConfigLoader();
            var config = loader.Load<CommandSetConfig>(TestConfig);

            Assert.AreEqual("TestCommandSet", config.Name);
            Assert.AreEqual("TestDirectory", config.ScriptDirectory);
            Assert.AreEqual("TestProjectDirectory", config.ProjectDirecotry);
        }

        [TestMethod]
        public void RootSearcherTest()
        {
        }
    }
}