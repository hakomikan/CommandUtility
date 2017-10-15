using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUtility;

namespace CommandInterface.Tests
{
    public class TestEnvironmentAccessor : IEnvironmentVariableAccessor
    {
        private Dictionary<string, string> Variables { get; set; }

        public TestEnvironmentAccessor()
        {
            Variables = new Dictionary<string, string>();
        }

        public void Set(string name, string value)
        {
            Variables[name] = value;
        }

        public override string Get(string name)
        {
            return Variables[name];
        }
    }

    [TestClass()]
    public class RootSearcherTests : CommandInterfaceTest.CommandInterfaceTestBase
    {
        [TestMethod]
        public void RootSearcherTest()
        {
            using (var tmpHolder = new TemporaryFileHolder(TestContext))
            {
                var appDir = tmpHolder.CreateDirectory("ApplicationDirectory");
                var rootDir = tmpHolder.CreateDirectory("DirectoryTreeRoot");
                tmpHolder.CreateFile(                   "DirectoryTreeRoot/RootFile.txt", "");
                tmpHolder.CreateDirectory(              "DirectoryTreeRoot/Sub");
                var curDir =  tmpHolder.CreateDirectory("DirectoryTreeRoot/Sub/Sub");
                var homeDir = tmpHolder.CreateDirectory("HomeDirectory");

                FileUtility.OpenDirectory(tmpHolder.WorkSpaceDirectory);

                Assert.Fail();
                var env = new TestEnvironmentAccessor();
                env.Set("HOME", homeDir.FullName);

                var searcher = new RootSearcher(
                    new ApplicationRelativeSearcher(appDir),
                    new DirectoryTreeSearcher(curDir, "RootFile.txt"),
                    new EnvironmentVariableSearcher("HOME", env),
                    new EnvironmentVariableSearcher("SCRIPT_PATH", env)
                    );
            }
        }
    }
}