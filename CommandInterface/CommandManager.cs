using System;
using System.IO;
using System.Linq;
using static CommandInterface.Utility.CSharpAssembly;
using static CommandInterface.Utility.NameConverter;

namespace CommandInterface
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
            return (from fileInfo in Config.ScriptDirectory.EnumerateFiles()
                    select ConvertToCommandName(fileInfo.Name)).ToArray();
        }

        public string GetScriptPath(string commandName)
        {
            var fileName = Path.Combine(Config.ScriptDirectory.FullName, ConvertToFileName(commandName));

            if (File.Exists(fileName))
            {
                return fileName;
            }
            else
            {
                throw new ScriptNotFoundException(commandName);
            }
        }

        public int Execute(string commandName, params string[] parameters)
        {
            var scriptFileInfo = new FileInfo(GetScriptPath(commandName));
            return ExecuteScriptFromFile(scriptFileInfo, parameters);
        }

        private Config Config;
    }
}