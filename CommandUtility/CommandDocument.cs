using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandUtility
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
            return string.Format("{0} {1}", document.Name, string.Join(" ", from argument in document.Arguments select argument.DocumentExpression));
        }
    }

    public class CommandArgumentDocument
    {
        public CommandArgumentInfo ArgumentInfo { get; set; }

        public CommandArgumentDocument(ParameterInfo parameter)
        {
            this.ArgumentInfo = new CommandArgumentInfo(parameter);
        }

        public string DocumentExpression
        {
            get
            {
                switch (ArgumentInfo.ArgumentType)
                {
                    case CommandArgumentType.Positional:
                        return WrapOptionalExpression(
                            string.Format("<{0}>", ArgumentInfo.Name));
                    case CommandArgumentType.Keyword:
                        return WrapOptionalExpression(
                            string.Format("{0} <value>", ArgumentInfo.GetOptionExpression()));
                    case CommandArgumentType.Flag:
                        return WrapOptionalExpression(
                            string.Format("{0}", ArgumentInfo.GetOptionExpression()));
                    default:
                        throw new Exception("unknown argument type: " + ArgumentInfo.ArgumentType.ToString());
                }
            }
        }

        private string WrapOptionalExpression(string expression)
        {
            if (ArgumentInfo.IsOptional)
            {
                return "[" + expression + "]";
            }
            else
            {
                return expression;
            }
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
}
