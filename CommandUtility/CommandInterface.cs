using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandUtility
{
    public class CommandInterface
    {
        private Type type;

        public CommandInterface(Type type)
        {
            this.type = type;
        }

        public CommandDocument GetDocument()
        {
            return new CommandDocument(type);
        }
    }

}
