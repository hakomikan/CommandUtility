﻿using System;
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
            var solutionPath = projectRoot.CombineAsFile(baseName + ".sln");
            var projectPath = projectRoot.CombineAsFile(baseName + ".csproj");
            var appConfigPath = projectRoot.CombineAsFile("App.config");
            var packagesConfigPath = projectRoot.CombineAsFile("packages.config");
            var assemblyInfoPath = projectRoot.CombineAsFile("Properties/AssemblyInfo.cs");
            var mainSourceCode = projectRoot.CombineAsFile("Program.cs");
            var projectGuid = Guid.NewGuid();
            var projectRelativePath = GetRelativePath(CommandInterfaceProject.FullName, projectRoot.FullName);
            var commandInterfaceProjectInfo = new ProjectInfo(CommandInterfaceProject);
            var commandInterfaceProjectGuid = ProjectInfo.ReadProjectGuid(CommandInterfaceProject);

            projectPath.Create(LoadAndWriteXml(ProjectTemplates.BasicTemplate, xdoc => {
                var rootNamespace = xdoc.Root.Name.Namespace;
                foreach (var srcFile in fileList)
                {
                    xdoc.Descendants().Where(e => e.Name.LocalName == "Compile").Last().AddAfterSelf(
                        new XElement(rootNamespace + "Compile",
                            new XAttribute("Include", GetRelativePath(srcFile.FullName, projectRoot.FullName))));
                }

                var guidElement = xdoc.Descendants().Where(e => e.Name.LocalName == "ProjectGuid").First();
                guidElement.Add(new XText($"{{{projectGuid.BraceExpression()}}}"));

                var configElement = xdoc.Descendants().Where(e => e.Name.LocalName == "ItemGroup" && e.Descendants().Where(f => f.Name.LocalName == "None").Count() != 0).First();
                configElement.AddAfterSelf(
                    new XElement(rootNamespace + "ItemGroup",
                        new XElement(rootNamespace + "ProjectReference",
                            new XAttribute("Include", projectRelativePath),
                            new XElement(rootNamespace + "Project", new XText(commandInterfaceProjectInfo.GuidText)),
                            new XElement(rootNamespace + "Name", new XText("CommandInterface"))
                    )));
            }));
            appConfigPath.Create(LoadAndWriteXml(ProjectTemplates.BasicAppConfig));
            packagesConfigPath.Create(LoadAndWriteXml(ProjectTemplates.BasicPackageConfig));
            assemblyInfoPath.Create(ProjectTemplates.BasicAssemblyInfo);
            mainSourceCode.Create(ProjectTemplates.BasicMainSourceCode);

            var solutionConstructor = new SolutionConstructor(solutionPath, new FileInfo[] { CommandInterfaceProject, projectPath });
            solutionConstructor.Create();

            ProjectPath = projectPath;
            SolutionPath = solutionPath;
        }
    }
}
