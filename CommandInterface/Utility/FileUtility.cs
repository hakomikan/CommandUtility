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

        public static string GetRelativePath(FileSystemInfo targetPath, FileInfo basePath)
        {
            return GetRelativePath(targetPath.FullName, Path.GetDirectoryName(basePath.FullName));
        }

        public static string GetRelativePath(FileSystemInfo targetPath, DirectoryInfo baseDirectory)
        {
            return GetRelativePath(targetPath.FullName, baseDirectory.FullName);
        }

        public static string GetRelativePath(string targetPath, string basePath)
        {
            Uri pathUri = new Uri(targetPath);
            if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                basePath += Path.DirectorySeparatorChar;
            }
            Uri baseUri = new Uri(basePath);
            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
