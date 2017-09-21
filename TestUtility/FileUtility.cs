using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace TestUtility
{
    public class FileUtility
    {
        public static void OpenDirectory(DirectoryInfo directory)
        {
            if(!Directory.Exists(directory.FullName))
            {
                throw new DirectoryNotFoundException($"{directory.FullName} is not found.");
            }

            System.Diagnostics.Process.Start(directory.FullName);
        }

        public static void OpenFile(FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.FullName))
            {
                throw new FileNotFoundException($"{fileInfo.FullName} is not found.");
            }

            System.Diagnostics.Process.Start(fileInfo.FullName);
        }
    }
}
