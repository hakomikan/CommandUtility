using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface
{
    public class CommandSetList
    {

    }

    public abstract class IEnvironmentVariableAccessor
    {
        public abstract string Get(string name);
    }

    public abstract class ISearcher
    {

    }

    public class ApplicationRelativeSearcher : ISearcher
    {
        private DirectoryInfo ApplicationDirectory;

        public ApplicationRelativeSearcher(DirectoryInfo appDir)
        {
            this.ApplicationDirectory = appDir;
        }
    }

    public class DirectoryTreeSearcher : ISearcher
    {
        private DirectoryInfo CurrentDirectory;
        private string RootFileName;

        public DirectoryTreeSearcher(DirectoryInfo currentDirectory, string rootFileName)
        {
            this.CurrentDirectory = currentDirectory;
            this.RootFileName = rootFileName;
        }
    }

    public class EnvironmentVariableSearcher : ISearcher
    {
        public EnvironmentVariableSearcher(string name, IEnvironmentVariableAccessor envAccessor)
        {
            EnvironmentVariables = envAccessor;
        }

        private IEnvironmentVariableAccessor EnvironmentVariables { get; set; }
    }

    public class RootSearcher
    {
        public RootSearcher(params ISearcher[] searchers)
        {
            Searchers = searchers.ToList();
        }

        public IEnumerable<CommandSetList> Enumerate()
        {
            return null;
        }

        public List<ISearcher> Searchers { get; private set; }
    }
}
