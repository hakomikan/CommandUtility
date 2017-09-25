﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandInterface.Utility.FileUtility;
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

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        private string LoadAndWriteXml(string xmlText, Action<XDocument> processor = null)
        {
            using (var reader = new StringReader(xmlText))
            using (var writer = new Utf8StringWriter())
            {
                var xdoc = XDocument.Load(reader);
                xdoc.Declaration = new XDeclaration("1.0", "utf-8", null);

                if (processor != null)
                {
                    processor(xdoc);
                }

                xdoc.Save(writer);
                return writer.ToString();
            }
        }

        public void CreateProject(string baseName, DirectoryInfo projectRoot, List<FileInfo> fileList)
        {
            var solutionPath = MakeFileInfo(projectRoot, baseName + ".sln");
            var projectPath = MakeFileInfo(projectRoot, baseName + ".csproj");
            var appConfigPath = MakeFileInfo(projectRoot, "App.config");
            var packagesConfigPath = MakeFileInfo(projectRoot, "packages.config");
            var assemblyInfoPath = MakeFileInfo(projectRoot, "./Properties/AssemblyInfo.cs");
            var mainSourceCode = MakeFileInfo(projectRoot, "Program.cs");

            CreateFile(solutionPath, ProjectTemplates.BasicSolution);
            CreateFile(projectPath, LoadAndWriteXml(ProjectTemplates.BasicTemplate, xdoc => {
                var rootNamespace = xdoc.Root.Name.Namespace;
                foreach (var srcFile in fileList)
                {
                    xdoc.Descendants().Where(e => e.Name.LocalName == "Compile").Last().AddAfterSelf(
                        new XElement(rootNamespace + "Compile",
                            new XAttribute("Include", GetRelativePath(srcFile.FullName, projectRoot.FullName))));
                }
            }));
            CreateFile(appConfigPath, LoadAndWriteXml(ProjectTemplates.BasicAppConfig));
            CreateFile(packagesConfigPath, LoadAndWriteXml(ProjectTemplates.BasicPackageConfig));
            CreateFile(assemblyInfoPath, ProjectTemplates.BasicAssemblyInfo);
            CreateFile(mainSourceCode, ProjectTemplates.BasicMainSourceCode);

            ProjectPath = projectPath;
            SolutionPath = solutionPath;
        }
    }
}
