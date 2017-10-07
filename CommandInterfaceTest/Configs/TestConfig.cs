using System;
using System.Collections.Generic;

namespace CommandInterface.Configs.Test
{
    public class Config : ScriptConfig
    {
        public Config()
        {
            SolutionPath = "testSolutionPath";
            ScriptPath = "testScriptPath";
            ProjectPath = "testProjectPath";
        }
    }
}
