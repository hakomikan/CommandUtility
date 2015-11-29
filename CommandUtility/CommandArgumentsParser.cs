using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandUtility
{
    public class ArgumentValue
    {
    }

    public class PositionalArgumentValue : ArgumentValue
    {
        private string v;

        public PositionalArgumentValue(string v)
        {
            this.v = v;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.v == ((PositionalArgumentValue)obj).v;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class FlagArgumentValue : ArgumentValue
    {
        private string name;

        public FlagArgumentValue(string name)
        {
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.name == ((FlagArgumentValue)obj).name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class KeywordArguentValue : ArgumentValue
    {
        private string name;
        private string value;

        public KeywordArguentValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var v = (KeywordArguentValue)obj;

            return name == v.name && value == v.value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

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

        public List<SingleArgumentParser> FlagArguments
        {
            get
            {
                return (from argument in Arguments where argument.ArgumentInfo.IsFlagArgument select argument).ToList();
            }
        }

        public List<SingleArgumentParser> KeywordArguments
        {
            get
            {
                return (from argument in Arguments where argument.ArgumentInfo.IsKeywordArgument select argument).ToList();
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

        public List<ArgumentValue> DivideArguments(string[] v)
        {
            var ret = new List<ArgumentValue>() { };
            var enumerator = v.AsEnumerable().GetEnumerator();
            while(enumerator.MoveNext())
            {
                var current = enumerator.Current;
                switch (IdentifyArgumentType(current))
                {
                    case CommandArgumentType.Positional:
                        ret.Add(new PositionalArgumentValue(current));
                        break;
                    case CommandArgumentType.Keyword:
                        if(enumerator.MoveNext())
                        {
                            ret.Add(new KeywordArguentValue(current, enumerator.Current));
                        }
                        else
                        {
                            throw new LackKeywordArgumentValueException("lack value after: " + current);
                        }
                        break;
                    case CommandArgumentType.Flag:
                        ret.Add(new FlagArgumentValue(current));
                        break;
                    default:
                        throw new Exception("unknown arguent type: " + v);
                }
            }
            return ret;
        }

        public IEnumerable<string> ApplyFlagArgumentValue(IEnumerable<string> arguments, Action<string> action)
        {
            var enumerator = arguments.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if(IsFlagArgumentValue(current))
                {
                    action(current);
                }
                else
                {
                    yield return current;
                }
            }
        }

        public IEnumerable<string> ApplyKeywordArgumentValue(IEnumerable<string> arguments, Action<string, string> action)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (IsKeywordArgumentValue(current))
                {
                    if (enumerator.MoveNext())
                    {
                        action(current, enumerator.Current);
                    }
                    else
                    {
                        throw new LackKeywordArgumentValueException("lack value after: " + current);
                    }
                }
                else
                {
                    yield return current;
                }
            }
        }

        public IEnumerable<string> ApplyPositionalArgumentValue(IEnumerable<string> arguments, Action<string> action)
        {
            var argumentEnumerator = PositionalArguments.GetEnumerator();
            var valueEnumerator = arguments.GetEnumerator();
            while (argumentEnumerator.MoveNext() && valueEnumerator.MoveNext())
            {
                action(valueEnumerator.Current);
            }

            while(valueEnumerator.MoveNext())
            {
                yield return valueEnumerator.Current;
            }
        }

        public CommandArgumentType IdentifyArgumentType(string v)
        {
            if(IsKeywordArgumentValue(v))
            {
                return CommandArgumentType.Keyword;
            }

            if(IsFlagArgumentValue(v))
            {
                return CommandArgumentType.Flag;
            }

            if(IsPositionalArgumentValue(v))
            {
                return CommandArgumentType.Positional;
            }

            throw new NotImplementedException();
        }

        public bool IsKeywordArgumentValue(string v)
        {
            foreach (var argument in KeywordArguments)
            {
                if(argument.ArgumentInfo.GetOptionExpression() == v)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFlagArgumentValue(string v)
        {
            foreach(var argument in FlagArguments)
            {
                if(argument.ArgumentInfo.GetOptionExpression() == v)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsPositionalArgumentValue(string v)
        {
            foreach (var argument in Arguments)
            {
                if (argument.ArgumentInfo.ArgumentType != CommandArgumentType.Positional &&
                    argument.ArgumentInfo.GetOptionExpression() == v)
                {
                    return false;
                }
            }

            return true;
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