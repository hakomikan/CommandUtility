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

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class ProjectConstructorTests : CommandInterfaceTest.CommandInterfaceTestBase
    {
        [TestMethod()]
        public void ProjectConstructorTest()
        {
            // TODO: テスト用の作業スペースを使うようにする
            using (var holder = new TemporaryFileHolder("ProjectConstructor"))
            {
                // もととなるスクリプトファイルを用意する
                var script1 = holder.MakeCopiedFile("./Scripts/TestScript.cs", CommandManager.GetScriptPath("test-script"));
                var script2 = holder.MakeCopiedFile("./Scripts/TestScript2.cs", CommandManager.GetScriptPath("test-script2"));
                var projectPath = holder.MakeFilePath("./TestProject.csproj");

                var constructor = new ProjectConstructor();

                // プロジェクトを生成する
                constructor.CreateProject(
                    projectPath,
                    new List<FileInfo>() { script1, script2 });

                // 生成されたプロジェクトファイルを確認する
                Assert.IsTrue(File.Exists(constructor.ProjectPath.FullName));
            }

            // TODO: 出来上がったプロジェクトを消さずにフォルダを開くようにする
            throw new NotImplementedException();
        }
    }
}