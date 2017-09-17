using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestUtility
{
    public class TemporaryFileHolder : IDisposable
    {
        public TemporaryFileHolder(string name = "")
        {
            WorkSpaceDirectory = MakeWorkSpaceDirectory(
                new DirectoryInfo(Path.GetTempPath()),
                name);

            EnsureWorkSpace(WorkSpaceDirectory);
        }

        public TemporaryFileHolder(DirectoryInfo rootDirectory, string name = "")
        {
            WorkSpaceDirectory = MakeWorkSpaceDirectory(
                rootDirectory,
                name);

            EnsureWorkSpace(WorkSpaceDirectory);
        }

        public DirectoryInfo WorkSpaceDirectory { get; private set; }

        public void Dispose()
        {
            if(Directory.Exists(WorkSpaceDirectory.FullName))
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
