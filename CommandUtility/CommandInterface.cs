using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandUtility
{
    public class CommandInterface<T> where T : class, new()
    {
        private Type type;

        public CommandInterface()
        {
            this.type = typeof(T);
            Parser = new CommandLineParser(typeof(T));
            Invoker = new CommandInvoker<T>();
        }

        public CommandDocument GetDocument()
        {
            return new CommandDocument(type);
        }

        public CommandLineParser Parser { get; set; }
        public CommandInvoker<T> Invoker { get; set; }

        public int Run(string[] arguments)
        {
            return Invoker.Invoke(Parser.Parse(arguments).FunctionArguments);
        }
    }

}
