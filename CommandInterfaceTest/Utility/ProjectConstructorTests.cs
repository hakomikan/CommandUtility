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

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class ProjectConstructorTests : CommandInterfaceTest.CommandInterfaceTestBase
    {
        [TestMethod()]
        public void ProjectConstructorTest()
        {
            // TODO: テスト用の作業スペースを使うようにする
            using (var holder = new TemporaryFileHolder("ProjectConstructor", afterDelete: false))
            {
                var script1 = holder.MakeCopiedFile("./Scripts/TestScript.cs", CommandManager.GetScriptPath("test-script"));
                var script2 = holder.MakeCopiedFile("./Scripts/TestScript2.cs", CommandManager.GetScriptPath("test-script2"));
                var projectPath = holder.MakeFilePath("./TestProject.csproj");
                var solutionPath = holder.MakeFilePath("./TestSolution.sln");

                var constructor = new ProjectConstructor();
                constructor.CreateProject(
                    projectPath,
                    solutionPath,
                    new List<FileInfo>() { script1, script2 });

                Assert.IsTrue(File.Exists(constructor.ProjectPath.FullName));

                TestUtility.FileUtility.OpenDirectory(holder.WorkSpaceDirectory);
            }

            throw new NotImplementedException();
        }
    }
}