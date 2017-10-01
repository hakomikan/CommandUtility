using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface.Utility
{
    public class SolutionEntry
    {
        public ProjectInfo ProjectInfo { get; private set; }

        public SolutionEntry(FileInfo projectPath)
        {
            ProjectInfo = new ProjectInfo(projectPath);
        }

        public string MakeProjectDefinitonText(FileInfo solutionPath)
        {
            var relativeProjectPath = FileUtility.GetRelativePath(ProjectInfo.Path, solutionPath);
            return $@"Project(""{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}"") = ""{ProjectInfo.Name}"", ""{relativeProjectPath}"", ""{{{ProjectInfo.GuidText}}}""
EndProject";
        }

        public string MakeProjectConfigurationText()
        {
            return $@"{ProjectInfo.GuidText}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
{ProjectInfo.GuidText}.Debug|Any CPU.Build.0 = Debug|Any CPU
{ProjectInfo.GuidText}.Release|Any CPU.ActiveCfg = Release|Any CPU
{ProjectInfo.GuidText}.Release|Any CPU.Build.0 = Release|Any CPU";
        }
    }
}
