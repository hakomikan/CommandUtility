using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandInterface.Utility.FileUtility;
using static CommandInterface.Utility.Utility;
using System.Xml.Linq;

namespace CommandInterface.Utility
{
    public class ProjectConstructor
    {
        public ProjectConstructor()
        {

        }

        public FileInfo ProjectPath { get; set; }
        public FileInfo SolutionPath { get; set; }
        public FileInfo CommandInterfaceProject { get; set; }

        public void CreateProject(string baseName, DirectoryInfo projectRoot, List<FileInfo> fileList)
        {
            var solutionPath = MakeFileInfo(projectRoot, baseName + ".sln");
            var projectPath = MakeFileInfo(projectRoot, baseName + ".csproj");
            var appConfigPath = MakeFileInfo(projectRoot, "App.config");
            var packagesConfigPath = MakeFileInfo(projectRoot, "packages.config");
            var assemblyInfoPath = MakeFileInfo(projectRoot, "./Properties/AssemblyInfo.cs");
            var mainSourceCode = MakeFileInfo(projectRoot, "Program.cs");
            var projectGuid = Guid.NewGuid();
            var projectRelativePath = GetRelativePath(CommandInterfaceProject.FullName, projectRoot.FullName);
            var commandInterfaceProjectGuid = ProjectInfo.ReadProjectGuid(CommandInterfaceProject);

            CreateFile(projectPath, LoadAndWriteXml(ProjectTemplates.BasicTemplate, xdoc => {
                var rootNamespace = xdoc.Root.Name.Namespace;
                foreach (var srcFile in fileList)
                {
                    xdoc.Descendants().Where(e => e.Name.LocalName == "Compile").Last().AddAfterSelf(
                        new XElement(rootNamespace + "Compile",
                            new XAttribute("Include", GetRelativePath(srcFile.FullName, projectRoot.FullName))));
                }

                var guidElement = xdoc.Descendants().Where(e => e.Name.LocalName == "ProjectGuid").First();
                guidElement.Add(new XText($"{{{projectGuid.ToString().ToUpper()}}}"));

                var configElement = xdoc.Descendants().Where(e => e.Name.LocalName == "ItemGroup" && e.Descendants().Where(f => f.Name.LocalName == "None").Count() != 0).First();
                configElement.AddAfterSelf(
                    new XElement(rootNamespace + "ItemGroup",
                        new XElement(rootNamespace + "ProjectReference",
                            new XAttribute("Include", projectRelativePath),
                            new XElement(rootNamespace + "Project", new XText($"{{{commandInterfaceProjectGuid.ToString().ToUpper()}}}")),
                            new XElement(rootNamespace + "Name", new XText("CommandInterface"))
                    )));
            }));
            CreateFile(appConfigPath, LoadAndWriteXml(ProjectTemplates.BasicAppConfig));
            CreateFile(packagesConfigPath, LoadAndWriteXml(ProjectTemplates.BasicPackageConfig));
            CreateFile(assemblyInfoPath, ProjectTemplates.BasicAssemblyInfo);
            CreateFile(mainSourceCode, ProjectTemplates.BasicMainSourceCode);

            var solutionConstructor = new SolutionConstructor(solutionPath, new FileInfo[] { CommandInterfaceProject, projectPath });
            solutionConstructor.Create();

            ProjectPath = projectPath;
            SolutionPath = solutionPath;
        }
    }
}
