using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandInterface;
using System.IO;
using TestUtility;
using System.Diagnostics;
using CommandInterfaceTest;

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class ProjectConstructorTests : CommandInterfaceTestBase
    {
        [TestMethod()]
        public void ProjectConstructorTest()
        {
            using (var holder = new TemporaryFileHolder(TestContext))
            {
                var script1 = holder.MakeCopiedFile("./Scripts/TestScript.cs", CommandManager.GetScriptPath("test-script"));
                var script2 = holder.MakeCopiedFile("./Scripts/TestScript2.cs", CommandManager.GetScriptPath("test-script2"));

                var constructor = new ProjectConstructor();
                constructor.CreateProject(
                    "TestProject",
                    holder.WorkSpaceDirectory,
                    new List<FileInfo>() { script1, script2 });

                Assert.IsTrue(File.Exists(constructor.ProjectPath.FullName));

                TestUtility.FileUtility.OpenDirectory(holder.WorkSpaceDirectory);
                //TestUtility.FileUtility.OpenFile(constructor.SolutionPath);
            }

            throw new NotImplementedException();
        }
    }
}