using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandInterfaceTest
{
    public class CommandInterfaceTestBase
    {
        public TestContext TestContext { get; set; }

        public CommandInterfaceTestBase()
        {
            this.CommandManager = new CommandManager(GetTestConfig());
        }

        public CommandManager CommandManager { get; set; }

        public DirectoryInfo GetScriptDirectory()
        {
            return new DirectoryInfo(
                Path.GetFullPath(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "../../Scripts")));
        }

        public string GetTestScriptPath(string filename)
        {
            return Path.Combine(GetScriptDirectory().FullName, filename);
        }

        public Config GetTestConfig()
        {
            return new Config()
            {
                ScriptDirectory = GetScriptDirectory()
            };
        }
    }
}
