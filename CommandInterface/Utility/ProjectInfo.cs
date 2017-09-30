using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace CommandInterface.Utility
{
    public class ProjectInfo
    {
        public FileInfo Path { get; private set; }
        public Guid Guid { get; private set; }

        public string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(Path.FullName);
            }
        }

        public ProjectInfo(FileInfo projectPath)
        {
            Path = projectPath;
            Guid = ReadProjectGuid(projectPath);
        }

        public string GuidText
        {
            get
            {
                return $"{{{Guid.ToString().ToUpper()}}}";
            }
        }

        public static Guid ReadProjectGuid(FileInfo projectPath)
        {
            using (var reader = projectPath.OpenText())
            {
                var xdoc = XDocument.Load(reader);
                var guidString = xdoc.Descendants().Where(e => e.Name.LocalName == "ProjectGuid").First().Value;
                return System.Guid.Parse(guidString.Trim('{','}'));
            }
        }
    }
}
