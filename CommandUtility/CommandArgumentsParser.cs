using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandUtility
{
    public class CommandArgumentsParser
    {
        private Type type;

        public CommandArgumentsParser(Type type)
        {
            this.type = type;
            this.Arguments = (
                from parameter 
                in type.GetMethod("Main").GetParameters()
                select new SingleArgumentParser(parameter)).ToList();
        }

        public List<SingleArgumentParser> Arguments { get; set; }
        public List<SingleArgumentParser> PositionalArguments
        {
            get
            {
                return (from argument in Arguments where argument.ArgumentInfo.IsPositionalArgument select argument).ToList();
            }
        }

        public List<object> ParseV(params string[] arguments)
        {
            return Parse(arguments);
        }

        public List<object> Parse(string[] arguments)
        {
            string[] restArguments = ParseOptionArgument(arguments);

            if (restArguments.Count() < PositionalArguments.Count())
            {
                throw new LackPositionalArgumentException(string.Format("Too few arguments. expected = {0}, actual = {1}", Arguments.Count(), arguments.Count()));
            }
            else if (restArguments.Count() > PositionalArguments.Count())
            {
                throw new TooManyPositionalArgumentException(string.Format("Too many arguments. expected = {0}, actual = {1}", Arguments.Count(), arguments.Count()));
            }

            return (
                from parsed 
                in restArguments.Zip(Arguments, (value, definedAgument) => definedAgument.Parse(value))
                select parsed).ToList();
        }

        private string[] ParseOptionArgument(string[] arguments)
        {
            List<string> ret = new List<string>();

            foreach (var argument in arguments)
            {
                if(!argument.StartsWith("--"))
                {
                    ret.Add(argument);
                }
            }

            return ret.ToArray();
        }
    }

    public class SingleArgumentParser
    {
        public CommandArgumentInfo ArgumentInfo { get; set; }

        public SingleArgumentParser(ParameterInfo parameter)
        {
            this.ArgumentInfo = new CommandArgumentInfo(parameter);
        }

        public object Parse(string v)
        {
            if(ArgumentInfo.ValueType == typeof(int))
            {
                try
                {
                    return int.Parse(v);
                }
                catch
                {
                    throw new InvalidTypeArgumentException(string.Format("Invalid type argument: expected: integer, actual = {0}", v));
                }
            }
            else if(ArgumentInfo.ValueType == typeof(string))
            {
                return v;
            }
            else
            {
                throw new Exception(string.Format("Can't parse. type = {0}, value = {1}", ArgumentInfo.ValueType, v));
            }
        }
    }
}