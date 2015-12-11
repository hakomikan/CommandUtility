using System;
using System.Reflection;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class DocumentTest
    {
        class TestCommand
        {
            public static int Main(int numberArgument, string stringArgument = "aaa")
            {
                return 0;
            }
        }

        class TestCommand2
        {
            public static int Main(int numberArgument, string stringArgument, bool flagArgument)
            {
                return 0;
            }
        }

        private CommandDocument document = new CommandDocument(typeof(TestCommand));
        private CommandDocument document2 = new CommandDocument(typeof(TestCommand2));

        [TestMethod]
        public void TestGetCommandDocument()
        {
            Assert.AreEqual("TestCommand", document.Name);
            Assert.AreEqual("TestCommand2", document2.Name);

            CollectionAssert.AreEqual(new string[] { "numberArgument", "stringArgument" },
                (from argument in document.Arguments select argument.ArgumentInfo.Name).ToArray());
            CollectionAssert.AreEqual(new string[] { "numberArgument", "stringArgument", "flagArgument"},
                (from argument in document2.Arguments select argument.ArgumentInfo.Name).ToArray());
        }

        [TestMethod]
        public void TestGetArgumentType()
        {
            Assert.IsFalse(document.Arguments[0].ArgumentInfo.IsFlagArgument);
            Assert.IsFalse(document.Arguments[0].ArgumentInfo.IsKeywordArgument);
            Assert.IsTrue(document.Arguments[0].ArgumentInfo.IsPositionalArgument);
            Assert.IsFalse(document.Arguments[1].ArgumentInfo.IsFlagArgument);
            Assert.IsTrue(document.Arguments[1].ArgumentInfo.IsKeywordArgument);
            Assert.IsTrue(document2.Arguments[2].ArgumentInfo.IsFlagArgument);
            Assert.AreEqual(CommandArgumentType.Positional, document.Arguments[0].ArgumentInfo.ArgumentType);
            Assert.AreEqual(CommandArgumentType.Keyword, document.Arguments[1].ArgumentInfo.ArgumentType);
            Assert.AreEqual(CommandArgumentType.Positional, document2.Arguments[1].ArgumentInfo.ArgumentType);
            Assert.AreEqual(CommandArgumentType.Flag, document2.Arguments[2].ArgumentInfo.ArgumentType);
        }

        [TestMethod]
        public void TestGetArgumentExpression()
        {
            Assert.AreEqual("<numberArgument>", document.Arguments[0].DocumentExpression);
            Assert.AreEqual("[--string-argument <value>]", document.Arguments[1].DocumentExpression);
            Assert.AreEqual("<stringArgument>", document2.Arguments[1].DocumentExpression);
            Assert.AreEqual("[--flag-argument]", document2.Arguments[2].DocumentExpression);
        }

        [TestMethod]
        public void TestIsRequired()
        {
            Assert.IsTrue(document.Arguments[0].ArgumentInfo.IsRequired);
            Assert.IsFalse(document.Arguments[1].ArgumentInfo.IsRequired);
            Assert.IsTrue(document2.Arguments[1].ArgumentInfo.IsRequired);
            Assert.IsFalse(document2.Arguments[2].ArgumentInfo.IsRequired);
        }

        [TestMethod]
        public void TestGetTextDocument()
        {
            var document = new CommandDocument(typeof(TestCommand));
            var document2 = new CommandDocument(typeof(TestCommand2));

            CommandDocumentGenerator generator = new CommandDocumentGenerator(document);
            Assert.AreEqual(@"TestCommand <numberArgument> [--string-argument <value>]", generator.MakeTextDocument());

            CommandDocumentGenerator generator2 = new CommandDocumentGenerator(document2);
            Assert.AreEqual(@"TestCommand2 <numberArgument> <stringArgument> [--flag-argument]", generator2.MakeTextDocument());
        }
    }

}