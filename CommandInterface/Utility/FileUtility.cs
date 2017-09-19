using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface.Utility
{
    public static class FileUtility
    {
        public static FileInfo MakeFileInfo(DirectoryInfo directory, string filename)
        {
            return new FileInfo(Path.Combine(directory.FullName, filename));
        }

        public static void CreateFile(FileInfo fileInfo, string text)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileInfo.FullName));
            using (var writer = fileInfo.CreateText())
            {
                writer.Write(text);
            }
        }
    }
}
