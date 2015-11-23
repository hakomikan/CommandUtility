﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Collections.Generic;

namespace CommandUtilityTest
{
    [TestClass]
    public class ParserTest
    {
        class TestCommand
        {
            public static int Main(int numberArgument)
            {
                return 0;
            }
        }

        class TestCommand2
        {
            public static int Main(string stringArgument)
            {
                return 0;
            }
        }

        class TestCommand3
        {
            public static int Main(string stringArgument, int numberArgument)
            {
                return 0;
            }
        }

        class TestOneFlagCommand
        {
            public static int Main(bool flagArgument)
            {
                return 0;
            }
        }

        CommandArgumentsParser parser = new CommandArgumentsParser(typeof(TestCommand));
        CommandArgumentsParser parser2 = new CommandArgumentsParser(typeof(TestCommand2));
        CommandArgumentsParser parser3 = new CommandArgumentsParser(typeof(TestCommand3));
        CommandArgumentsParser flagArgumentParser = new CommandArgumentsParser(typeof(TestOneFlagCommand));

        [TestMethod]
        public void TestFirstParser()
        {
            Assert.AreEqual(1234, (int)parser.ParseV("1234")[0]);
            Assert.AreEqual(2345, (int)parser.ParseV("2345")[0]);
            Assert.AreEqual("testString", (string)parser2.ParseV("testString")[0]);
            CollectionAssert.AreEqual(
                new List<object>() { "1234", 1234 },
                parser3.ParseV("1234", "1234"));
        }

        [TestMethod]
        public void TestSinglePositionalArgumentParser()
        {
            Assert.AreEqual(1234, (int)parser.Arguments[0].Parse("1234"));
            Assert.AreEqual("1234", (string)parser2.Arguments[0].Parse("1234"));
        }

        [TestMethod]
        public void TestArgumentParserType()
        {
            Assert.IsTrue(parser.Arguments[0].ArgumentInfo.IsPositionalArgument);
            Assert.IsFalse(flagArgumentParser.Arguments[0].ArgumentInfo.IsPositionalArgument);
        }

        [TestMethod]
        public void TestLackArgument()
        {
            AssertUtility.Throws<LackPositionalArgumentException>(() =>
            {
                parser3.ParseV("1234");
            });
        }

        [TestMethod]
        public void TestTooManyArgument()
        {
            AssertUtility.Throws<TooManyPositionalArgumentException>(() =>
            {
                parser.ParseV("1234", "1234");
            });
        }

        [TestMethod]
        public void TestInvalidTypeArgument()
        {
            AssertUtility.Throws<InvalidTypeArgumentException>(() =>
            {
                parser.ParseV("aaaa");
            });
        }

        [TestMethod]
        public void TestArgumentMatch()
        {
            // 定義されたコマンドの引数を順番に捜査する
            // 与えられた引数とマッチしたらそれを消費する
            // デフォルト値を持つものはマッチしない場合にも値が入る
            // 次の引数も見ながら処理できる仕組みを作る。１つずれたZIPみたいなもの。
            // 引数を受け取って自分が処理すべき引数か判定するメソッドをテストする
            // 処理し終わった引数を管理する仕組みをテストする

            /// 引数のパース手順
            /// １．引数の単位で分割する（SlideZipを使う）
            ///     値引き数はそのまま
            ///     フラグの引数はフラグのまま
            ///     キーワード引数は値とセットで
            /// ２．
        }

        [TestMethod]
        [Ignore]
        public void TestFlagArgument()
        {
            CollectionAssert.AreEqual(
                new List<object>() { false },
                flagArgumentParser.ParseV());
            
            CollectionAssert.AreEqual(
                new List<object>() { true },
                flagArgumentParser.ParseV("--flag-argument"));
        }
    }
}
