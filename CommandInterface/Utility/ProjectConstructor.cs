using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface.Utility
{
    public class ProjectConstructor
    {
        public ProjectConstructor()
        {

        }

        public FileInfo ProjectPath { get; set; }

        public void CreateProject(FileInfo projectPath, List<FileInfo> list)
        {
            // テンプレート内のXML を順番に加工していく

            using (var writer = projectPath.CreateText())
            {
                writer.Write(ProjectTemplates.BasicTemplate);
            }

            ProjectPath = projectPath;
        }
    }
}
