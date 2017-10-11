using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestUtility
{
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
