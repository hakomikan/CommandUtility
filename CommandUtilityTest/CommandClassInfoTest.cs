using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using CommandUtility;

namespace CommandUtilityTest
{
    [TestClass]
    public class CommandClassInfoTest
    {
        [TestMethod]
        public void TestCommandClassInfo()
        {
            var commandInfo = new CommandClassInfo(typeof(TestMixCommand));
            var commandNoInfo = new CommandClassInfo(typeof(TestNoCommand));

            Assert.IsTrue(commandInfo.HasMainCommand);
            Assert.IsFalse(commandNoInfo.HasMainCommand);
        }
    }

    public class CommandClassInfo
    {
        private Type type;

        public CommandClassInfo(Type type)
        {
            this.type = type;
        }

        public bool HasMainCommand
        {
            get
            {
                return type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public) != null;
            }
        }

        public IEnumerable<MethodInfo> MainCommands
        {
            get
            {
                return from method
                       in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                       where method.Name == "Main"
                       select method;
            }
        }

        public MethodInfo MainCommand
        {
            get
            {
                var ret = MainCommands.FirstOrDefault();

                if(ret == null)
                {
                    throw new HasNoMainCommandException();
                }

                return ret;
            }
        }
    }
}
