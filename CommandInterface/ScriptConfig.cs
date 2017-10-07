using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface
{
    public class ScriptConfig
    {
        public string ScriptPath { get; set; }
        public string SolutionPath { get; set; }
        public string ProjectPath { get; set; }

        public ScriptConfig Load(FileInfo configFile)
        {

        }
    }
}
