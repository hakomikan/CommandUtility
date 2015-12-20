using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtilityTest
{
    class TestNoCommand
    {
    }

    class TestCommand
    {
        public int Main(int numberArgument)
        {
            return 0;
        }
    }

    class TestCommand2
    {
        public int Main(string stringArgument)
        {
            return 0;
        }
    }

    class TestCommand3
    {
        public int Main(string stringArgument, int numberArgument)
        {
            return 0;
        }
    }

    class TestOneFlagCommand
    {
        public int Main(bool flagArgument)
        {
            return 0;
        }
    }

    class TestOneKeywordCommand
    {
        public int Main(string keywordArgument = "defaultValue")
        {
            return 0;
        }
    }

    class TestMixCommand
    {
        public int Main(string stringArgument, int numberArgument, bool flagArgument, string keywordArgument = "defaultValue")
        {
            return 0;
        }
    }
}
