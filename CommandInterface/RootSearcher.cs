using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface
{
    public class CommandSetList
    {

    }

    public abstract class ISearcher
    {

    }

    public class ApplicationRelativeSearcher : ISearcher
    {

    }

    public class DirectoryTreeSearcher : ISearcher
    {

    }

    public class EnvironmentVariableSearcher : ISearcher
    {

    }

    public class RootSearcher
    {
        public RootSearcher(IEnumerable<ISearcher> searchers)
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
