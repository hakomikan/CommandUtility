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
    public class CommandArgumentTest
    {
        class TestCommand
        {
            public static int Main(int numberArgument, string stringArgument, bool flagArgument, int argumentWithDefault = 3, params string[] variableArguments)
            {
                return 0;
            }
        }

        ParameterInfo[] parameters = typeof(TestCommand).GetMethod("Main").GetParameters();

        [TestMethod]
        public void TestArgumentType()
        {
            Assert.IsTrue(new CommandArgumentInfo(parameters[0]).IsPositionalArgument);
            Assert.IsTrue(new CommandArgumentInfo(parameters[1]).IsPositionalArgument);
            Assert.IsFalse(new CommandArgumentInfo(parameters[2]).IsPositionalArgument);
            Assert.IsTrue(new CommandArgumentInfo(parameters[2]).IsFlagArgument);
            Assert.IsTrue(new CommandArgumentInfo(parameters[3]).IsKeywordArgument);
            Assert.IsTrue(new CommandArgumentInfo(parameters[4]).IsVariableArgument);
        }
    }
}
