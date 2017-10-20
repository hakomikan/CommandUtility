using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandInterface.Utility;

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
        public abstract IEnumerable<DirectoryInfo> Directories { get; }
    }

    public class ApplicationRelativeSearcher : ISearcher
    {
        private DirectoryInfo ApplicationDirectory;

        public ApplicationRelativeSearcher(DirectoryInfo appDir)
        {
            this.ApplicationDirectory = appDir;
        }

        public override IEnumerable<DirectoryInfo> Directories
        {
            get
            {
                yield return ApplicationDirectory;
            }
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

        public override IEnumerable<DirectoryInfo> Directories
        {
            get
            {
                for (var currentDirectory = CurrentDirectory;
                    currentDirectory != null;
                    currentDirectory = currentDirectory.Parent)
                {
                    var rootFilePath = currentDirectory.CombineAsFile(RootFileName);
                    if (rootFilePath.Exists)
                    {
                        yield return currentDirectory;
                    }
                }
            }
        }
    }

    public class EnvironmentVariableSearcher : ISearcher
    {
        public EnvironmentVariableSearcher(string name, IEnvironmentVariableAccessor envAccessor)
        {
            Name = name;
            EnvironmentVariables = envAccessor;
        }

        private string Name { get; set; }
        private IEnvironmentVariableAccessor EnvironmentVariables { get; set; }

        public override IEnumerable<DirectoryInfo> Directories
        {
            get
            {
                foreach (var directoryName in EnvironmentVariables.Get(Name).Split(';'))
                {
                    yield return new DirectoryInfo(directoryName);
                }
            }
        }
    }

    public class RootSearcher : ISearcher
    {
        public RootSearcher(params ISearcher[] searchers)
        {
            Searchers = searchers.ToList();
        }

        public IEnumerable<CommandSetList> EnumerateCommandSetList()
        {
            return null;
        }

        public override IEnumerable<DirectoryInfo> Directories
        {
            get
            {
                foreach (var searcher in Searchers)
                {
                    foreach (var dirInfo in searcher.Directories)
                    {
                        yield return dirInfo;
                    }
                }
            }
        }

        public List<ISearcher> Searchers { get; private set; }
    }
}
