using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandInterface.Utility.FileUtility;

namespace CommandInterface.Utility
{
    public class ProjectConstructor
    {
        public ProjectConstructor()
        {

        }

        public FileInfo ProjectPath { get; set; }

        public void CreateProject(FileInfo projectPath, FileInfo solutionPath, List<FileInfo> list)
        {
            // テンプレート内のXML を順番に加工していく

            using (var writer = projectPath.CreateText())
            {
                writer.Write(ProjectTemplates.BasicTemplate);
            }

            using (var writer = solutionPath.CreateText())
            {
                writer.Write(ProjectTemplates.BasicSolution);
            }

            ProjectPath = projectPath;
        }

        public void CreateProject(string baseName, DirectoryInfo projectRoot, List<FileInfo> list)
        {
            var solutionPath = MakeFileInfo(projectRoot, baseName + ".sln");
            var projectPath = MakeFileInfo(projectRoot, baseName + ".csproj");
            var appConfigPath = MakeFileInfo(projectRoot, "App.config");
            var packagesConfigPath = MakeFileInfo(projectRoot, "packages.config");
            var assemblyInfoPath = MakeFileInfo(projectRoot, "./Properties/AssemblyInfo.cs");
            var mainSourceCode = MakeFileInfo(projectRoot, "Program.cs");
            // テンプレート内のXML を順番に加工していく

            CreateFile(solutionPath, ProjectTemplates.BasicSolution);
            CreateFile(projectPath, ProjectTemplates.BasicTemplate);
            CreateFile(appConfigPath, ProjectTemplates.BasicAppConfig);
            CreateFile(packagesConfigPath, ProjectTemplates.BasicPackageConfig);
            CreateFile(assemblyInfoPath, ProjectTemplates.BasicAssemblyInfo);
            CreateFile(mainSourceCode, ProjectTemplates.BasicMainSourceCode);

            ProjectPath = projectPath;
        }
    }
}
