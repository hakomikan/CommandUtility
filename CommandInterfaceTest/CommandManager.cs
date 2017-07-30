using System;
using System.IO;
using System.Linq;

namespace CommandInterfaceTest
{
    public class Config
    {
        public DirectoryInfo ScriptDirectory { get; set; }
    }

    public class CommandManager
    {
        public CommandManager(Config config)
        {
            this.Config = config;
        }

        public string[] ListCommands()
        {
            return Config.ScriptDirectory.EnumerateFiles().Select(fileInfo => fileInfo.Name).ToArray();
        }

        private Config Config;
    }
}