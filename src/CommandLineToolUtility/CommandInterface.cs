using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLineToolUtility
{
    public class CommandDocumentGenerator
    {
        private CommandDocument document;

        public CommandDocumentGenerator(CommandDocument document)
        {
            this.document = document;
        }

        public string MakeTextDocument()
        {
            return string.Format("{0} {1}", document.Name, string.Join(" ", from argument in document.Arguments select string.Format("<{0}>", argument.Name)));
        }
    }

    public class CommandDocument
    {
        private Type type;

        public CommandDocument(Type type)
        {
            this.type = type;
            this.Name = type.Name;
            this.Arguments = (
                from parameter
                in type.GetMethod("Main").GetParameters()
                select new CommandArgumentDocument(parameter)).ToList();
        }

        public List<CommandArgumentDocument> Arguments { get; set; }
        public string Name { get; set; }
    }

    public class CommandArgumentDocument
    {
        private ParameterInfo parameter;

        public CommandArgumentDocument(ParameterInfo parameter)
        {
            this.parameter = parameter;
            this.Name = parameter.Name;
        }

        public object Name { get; set; }

        internal string GetKeywordArgumentExpression()
        {
            return "--number-argument";
        }

        internal string GetPositionalArgumentExpression()
        {
            return string.Format("<{0}>", Name);
        }
    }

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
