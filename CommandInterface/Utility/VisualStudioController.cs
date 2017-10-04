using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.IO;

namespace CommandInterface.Utility
{
    public class VisualStudioController
    {
        private EnvDTE.DTE DTE;

        public VisualStudioController()
        {
            DTE = System.Activator.CreateInstance(
                Type.GetTypeFromProgID("VisualStudio.DTE.14.0")) as EnvDTE.DTE;
        }

        public void OpenSolution(FileInfo solutionFile)
        {
            var solution = DTE.Solution;
            solution.Open(solutionFile.FullName);
            DTE.MainWindow.Activate();
        }

        public void OpenSourceFile(FileInfo fileInfo)
        {
            DTE.ItemOperations.OpenFile(fileInfo.FullName, EnvDTE.Constants.vsViewKindCode);
        }
    }
}
