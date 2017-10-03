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
        public VisualStudioController()
        {

        }

        public void OpenSolution(FileInfo solutionFile)
        {
            var dte = System.Activator.CreateInstance(
                Type.GetTypeFromProgID("VisualStudio.DTE.14.0")) as EnvDTE.DTE;
            var solution = dte.Solution;
            solution.Open(solutionFile.FullName);
            dte.MainWindow.Activate();
        }
    }
}
