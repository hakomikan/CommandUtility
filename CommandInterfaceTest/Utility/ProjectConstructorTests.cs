using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandInterface;
using System.IO;

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class ProjectConstructorTests
    {
        [TestMethod()]
        public void ProjectConstructorTest()
        {
            var constructor = new ProjectConstructor();

            // プロジェクトを生成する
            constructor.CreateProject();

            // 生成されたプロジェクトファイルを確認する
            Assert.IsTrue(File.Exists(constructor.ProjectPath));            
        }
    }
}