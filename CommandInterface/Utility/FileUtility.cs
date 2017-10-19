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

    public static class FileSystemInfoExtensions
    {
        public static string GetRelativePath(this FileSystemInfo targetPath, FileInfo basePath)
        {
            return FileUtility.GetRelativePath(
                targetPath.FullName, basePath.GetDirectoryPath().FullName);
        }

        public static string GetRelativePath(this FileSystemInfo targetPath, DirectoryInfo basePath)
        {
            return FileUtility.GetRelativePath(
                targetPath.FullName, basePath.FullName);
        }
    }

    public static class FileInfoExtensions
    {
        public static void Create(this FileInfo fileInfo, string text)
        {
            fileInfo.GetDirectoryPath().Create();
            using (var writer = fileInfo.CreateText())
            {
                writer.Write(text);
            }
        }

        public static DirectoryInfo GetDirectoryPath(this FileInfo fileInfo)
        {
            return new DirectoryInfo(Path.GetDirectoryName(fileInfo.FullName));
        }
    }

    public static class DirectoryInfoExtensions
    {
        public static FileInfo CombineAsFile(this DirectoryInfo directoryInfo, string filename)
        {
            return new FileInfo(Path.Combine(directoryInfo.FullName, filename));
        }

        public static DirectoryInfo CombineAsDirectory(this DirectoryInfo directoryInfo, string directoryName)
        {
            return new DirectoryInfo(Path.Combine(directoryInfo.FullName, directoryName));
        }
    }
}
