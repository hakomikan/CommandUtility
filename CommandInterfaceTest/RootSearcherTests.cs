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

                var searcher = new RootSearcher(
                    new ApplicationRelativeSearcher(),
                    new DirectoryTreeSearcher(),
                    new EnvironmentVariableSearcher(),
                    new EnvironmentVariableSearcher()
                    );
            }
        }
    }
}