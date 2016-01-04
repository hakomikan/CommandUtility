using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandUtility;
using System.Collections;

namespace CommandUtilityTest
{
    [TestClass]
    public class ArgumentParserTest
    {
        [TestMethod]
        public void TestArrayArgumentParser()
        {
            var argumentStore = TestParameters.IntegerParamsArgument.CreateArgumentStore();

            Assert.AreEqual(typeof(int[]), argumentStore.Get().GetType());

            Assert.IsFalse(argumentStore.HasValue);
            argumentStore.Store(1);
            Assert.IsTrue(argumentStore.HasValue);
            argumentStore.Store(2);
            CollectionAssert.AreEqual(new int[] { 1, 2 }, (int[])argumentStore.Get());
            argumentStore.Store(3);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, (int[])argumentStore.Get());
        }

        [TestMethod]
        public void TestListArgumentParser()
        {
            var argumentStore = TestParameters.IntegerListArgument.CreateArgumentStore();

            Assert.AreEqual(typeof(List<int>), argumentStore.Get().GetType());

            Assert.IsFalse(argumentStore.HasValue);
            argumentStore.Store(1);
            Assert.IsTrue(argumentStore.HasValue);
            argumentStore.Store(2);
            CollectionAssert.AreEqual(new int[] { 1, 2 }, (List<int>)argumentStore.Get());
            argumentStore.Store(3);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, (List<int>)argumentStore.Get());
        }

        [TestMethod]
        public void TestSingleArgumentParser()
        {
            var argumentStore = TestParameters.NumberPositionalArgument.CreateArgumentStore();

            Assert.IsFalse(argumentStore.HasValue);
            argumentStore.Store(1);
            Assert.IsTrue(argumentStore.HasValue);

            AssertUtility.Throws<TooManyArgumentException>(() =>
            {
                argumentStore.Store(2);
            });
        }
    }
}
