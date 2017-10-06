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

        public void GoToLine(int line)
        {
            var selection = DTE.ActiveDocument.Selection as TextSelection;
            selection.GotoLine(line);
        }

        public void GoToPoint(Document document, TextPoint textPoint)
        {
            var selection = document.Selection as TextSelection;
            selection.MoveToPoint(textPoint);
        }

        public void GoToMethod(string name)
        {
            var document = DTE.ActiveDocument;
            foreach (CodeElement codeElement in EnumerateCodeElements(document.ProjectItem.FileCodeModel.CodeElements))
            {
                if (codeElement.Name == name)
                {
                    GoToPoint(document, codeElement.StartPoint);
                }
            }
        }

        public IEnumerable<CodeElement> EnumerateCodeElements()
        {
            foreach (var e in EnumerateCodeElements(DTE.ActiveDocument.ProjectItem.FileCodeModel.CodeElements))
            {
                yield return e;
            }
        }

        public IEnumerable<CodeElement> EnumerateCodeElements(CodeElements codeElements)
        {
            foreach (CodeElement codeElement in codeElements)
            {
                yield return codeElement;

                foreach (CodeElement subCodeElement in EnumerateCodeElements(codeElement.Children))
                {
                    yield return subCodeElement;
                }
            }
        }
    }
}
