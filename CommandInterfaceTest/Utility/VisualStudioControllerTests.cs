using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUtility;
using System.IO;

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class VisualStudioControllerTests : CommandInterfaceTest.CommandInterfaceTestBase
    {
        [TestMethod()]
        public void VisualStudioControllerTest()
        {
            using (var holder = new TemporaryFileHolder(TestContext))
            {
                var script1 = new FileInfo(CommandManager.GetScriptPath("test-script"));
                var script2 = new FileInfo(CommandManager.GetScriptPath("test-script2"));

                var constructor = new ProjectConstructor()
                {
                    CommandInterfaceProject = this.CommandInterfaceProject
                };

                constructor.CreateProject(
                    "TestProject",
                    holder.WorkSpaceDirectory,
                    new List<FileInfo>() { script1, script2 });

                Assert.IsTrue(File.Exists(constructor.ProjectPath.FullName));

                TestUtility.FileUtility.OpenDirectory(holder.WorkSpaceDirectory);

                var vsController = new VisualStudioController();
                vsController.OpenSolution(constructor.SolutionPath);
                vsController.OpenSourceFile(script1);
                Assert.Fail();

            }

        }
    }
}