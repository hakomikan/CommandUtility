using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CommandInterface.Utility.NameConverter;

namespace CommandInterfaceTest
{
    /// <summary>
    /// NameConverterTest の概要の説明
    /// </summary>
    [TestClass]
    public class NameConverterTest
    {
        public NameConverterTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext { get; set; }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
