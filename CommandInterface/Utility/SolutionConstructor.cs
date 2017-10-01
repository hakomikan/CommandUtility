using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface.Utility
{
    public class SolutionConstructor
    {
        public FileInfo SolutionPath { get; private set; }
        public FileInfo[] ProjectPaths { get; private set; }
        public SolutionEntry[] SolutionEntries { get; private set; }

        public SolutionConstructor(FileInfo solutionPath, FileInfo[] projectPaths)
        {
            SolutionPath = solutionPath;
            ProjectPaths = projectPaths;
            SolutionEntries = projectPaths.Select(
                projectPath => new SolutionEntry(projectPath)).ToArray();
        }

        public FileInfo Create()
        {
            using (var writer = SolutionPath.CreateText())
            {
                var text = Template
                    .ReplaceKeepingIndent("#<ProjectDefinitions>#", MakeProjectDefinitionText())
                    .ReplaceKeepingIndent("#<ProjectConfigurations>#", MakeProjectConfigurationText());
                writer.Write(text);
            }

            return SolutionPath;
        }

        private string MakeProjectDefinitionText()
        {
            return string.Join("\n",
                from entry in SolutionEntries
                select entry.MakeProjectDefinitonText(SolutionPath));
        }

        private string MakeProjectConfigurationText()
        {
            return string.Join("\n",
                from entry in SolutionEntries
                select entry.MakeProjectConfigurationText());
        }

        private static string Template = @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.24720.0
MinimumVisualStudioVersion = 10.0.40219.1
#<ProjectDefinitions>#
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		#<ProjectConfigurations>#
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";
    }
}
