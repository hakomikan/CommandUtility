using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtility
{
    public class TemporaryFileHolder : IDisposable
    {
        public DirectoryInfo WorkSpaceDirectory { get; private set; }
        public bool AfterDelete { get; private set; }

        public TemporaryFileHolder(TestContext context, bool afterDelete = false)
        {
            AfterDelete = afterDelete;
            WorkSpaceDirectory = new DirectoryInfo(Path.Combine(context.DeploymentDirectory, "WorkSpace"));
            EnsureWorkSpace(WorkSpaceDirectory);
        }

        public FileInfo CreateFile(string path, string content)
        {
            var fileInfo = WorkSpaceDirectory.CombineAsFile(path);
            using (var writer = fileInfo.CreateText())
            {
                writer.Write(content);
            }
            return fileInfo;
        }

        public DirectoryInfo CreateDirectory(string name)
        {
            var directoryInfo = WorkSpaceDirectory.CombineAsDirectory(name);
            directoryInfo.Create();
            return directoryInfo;
        }

        public TemporaryFileHolder(string name = "", bool afterDelete = true)
        {
            AfterDelete = afterDelete;
            WorkSpaceDirectory = MakeWorkSpaceDirectory(
                new DirectoryInfo(Path.GetTempPath()),
                name);

            EnsureWorkSpace(WorkSpaceDirectory);
        }

        public TemporaryFileHolder(DirectoryInfo rootDirectory, string name = "", bool afterDelete = true)
        {
            AfterDelete = afterDelete;
            WorkSpaceDirectory = MakeWorkSpaceDirectory(
                rootDirectory,
                name);

            EnsureWorkSpace(WorkSpaceDirectory);
        }

        public FileInfo MakeCopiedFile(string destPath, string srcPath)
        {
            var destFullPath = Path.Combine(WorkSpaceDirectory.FullName, destPath);
            var srcFullPath = Path.GetFullPath(srcPath);

            EnsureDirectory(destFullPath);

            File.Copy(srcFullPath, destFullPath);

            return new FileInfo(destFullPath);
        }

        public FileInfo MakeFilePath(string path)
        {
            return new FileInfo(Path.Combine(WorkSpaceDirectory.FullName, path));
        }

        private DirectoryInfo EnsureDirectory(string targetFilePath)
        {
            return EnsureDirectory(new FileInfo(targetFilePath));
        }

        private DirectoryInfo EnsureDirectory(FileInfo targetFilePath)
        {
            var dirName = Path.GetDirectoryName(targetFilePath.FullName);

            if(Directory.Exists(dirName))
            {
                return new DirectoryInfo(dirName);
            }
            else
            {
                return Directory.CreateDirectory(dirName);
            }
        }

        public void Dispose()
        {
            if(Directory.Exists(WorkSpaceDirectory.FullName) && AfterDelete)
            {
                WorkSpaceDirectory.Delete(true);
            }
        }

        private DirectoryInfo MakeWorkSpaceDirectory(DirectoryInfo root, string name)
        {
            if(name != "")
            {
                return new DirectoryInfo(
                    Path.Combine(
                        root.FullName,
                        name + "_" + Path.GetRandomFileName()));
            }
            else
            {
                return new DirectoryInfo(
                    Path.Combine(
                        root.FullName,
                        Path.GetRandomFileName()));
            }
        }

        private void EnsureWorkSpace(DirectoryInfo workSpaceDirectory)
        {
            if (Directory.Exists(WorkSpaceDirectory.FullName))
            {
                throw new DirectoryAlreadyExistException(WorkSpaceDirectory);
            }
        }
    }


    [Serializable]
    public class DirectoryAlreadyExistException : Exception
    {
        private DirectoryInfo workSpaceDirectory;

        public DirectoryAlreadyExistException(DirectoryInfo workSpaceDirectory)
            : base($"Temporary directory(path={workSpaceDirectory.FullName}) is already exist.")
        {
            this.workSpaceDirectory = workSpaceDirectory;
        }
    }
}
