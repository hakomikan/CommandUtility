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
                in type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public).GetParameters()
                select new SingleArgumentParser(parameter)).ToList();
        }

        public List<SingleArgumentParser> Arguments { get; set; }

        public List<SingleArgumentParser> SequentialArguments
        {
            get
            {
                return (from argument in Arguments where argument.ArgumentInfo.IsSequentialArgument select argument).ToList();
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

        public List<SingleArgumentParser> VariableArguments
        {
            get
            {
                return (from argument in Arguments where argument.ArgumentInfo.IsVariableArgument select argument).ToList();
            }
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

        public IEnumerable<string> ApplyFlagArgumentValue(IEnumerable<string> arguments, Action<SingleArgumentParser> action)
        {
            var enumerator = arguments.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var current = enumerator.Current;

                var argumentParser = MatchFlagArgument(current);
                if(argumentParser != null)
                {
                    action(argumentParser);
                }
                else
                {
                    yield return current;
                }
            }
        }

        public IEnumerable<string> ApplyKeywordArgumentValue(IEnumerable<string> arguments, Action<SingleArgumentParser, string> action)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                var argumentParser = MatchKeywordArgument(current);
                if (argumentParser != null)
                {
                    if (enumerator.MoveNext())
                    {
                        action(argumentParser, enumerator.Current);
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

        public IEnumerable<string> ApplySequentialArgumentValue(IEnumerable<string> arguments, Action<SingleArgumentParser, string> action)
        {
            var argumentEnumerator = SequentialArguments.GetEnumerator();
            var valueEnumerator = arguments.GetEnumerator();
            while (argumentEnumerator.MoveNext() && valueEnumerator.MoveNext())
            {
                action(argumentEnumerator.Current, valueEnumerator.Current);

                if (argumentEnumerator.Current.ArgumentInfo.IsMultiple)
                {
                    while (valueEnumerator.MoveNext())
                    {
                        action(argumentEnumerator.Current, valueEnumerator.Current);
                    }
                }
            }

            while(valueEnumerator.MoveNext())
            {
                yield return valueEnumerator.Current;
            }
        }

        public SingleArgumentParser MatchFlagArgument(string v)
        {
            foreach (var argument in FlagArguments)
            {
                if (argument.ArgumentInfo.GetOptionExpression() == v)
                {
                    return argument;
                }
            }

            return null;
        }

        public SingleArgumentParser MatchKeywordArgument(string v)
        {
            foreach (var argument in KeywordArguments)
            {
                if (argument.ArgumentInfo.GetOptionExpression() == v)
                {
                    return argument;
                }
            }
            return null;
        }

        public Dictionary<SingleArgumentParser, string> StoreRawArguments(string[] v)
        {
            var ret = new Dictionary<SingleArgumentParser, string>();

            var restArgs = ApplyKeywordArgumentValue(v, (arg, value) => { ret[arg] = value; });
            var restArgs2 = ApplyFlagArgumentValue(restArgs, (arg) => { ret[arg] = ""; });
            var restArgs3 = ApplySequentialArgumentValue(restArgs2, (arg, value) => { ret[arg] = value; }).ToList();

            if(restArgs3.Count() != 0)
            {
                throw new TooManyArgumentException();
            }

            return ret;
        }

        public Dictionary<SingleArgumentParser, object> StoreParsedArguments(string[] v)
        {
            var ret = new Dictionary<SingleArgumentParser, dynamic>();

            var restArgs = ApplyKeywordArgumentValue(v, (arg, value) => { ret[arg] = arg.Parse(value); });
            var restArgs2 = ApplyFlagArgumentValue(restArgs, (arg) => { ret[arg] = true; });
            var restArgs3 = ApplySequentialArgumentValue(restArgs2, (arg, value) => {
                if(arg.ArgumentInfo.IsMultiple)
                {
                    if (ret.ContainsKey(arg))
                    {
                        ret[arg].Add(value);
                    }
                    else
                    {
                        // ここは AnyType[] を適切にリストにしないといけない
                        ret[arg] = new List<object>() { arg.Parse(value) };
                    }
                }
                else
                {
                    ret[arg] = arg.Parse(value);
                }
            }).ToList();

            if (restArgs3.Count() != 0)
            {
                throw new TooManyArgumentException();
            }

            return ret;
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

        public List<object> ParseAsFunctionArguments(string[] v)
        {
            var storeArgs = StoreParsedArguments(v);

            var ret = new List<object>();
            foreach (var argumentParser in Arguments)
            {
                if(storeArgs.ContainsKey(argumentParser))
                {
                    ret.Add(storeArgs[argumentParser]);
                }
                else
                {
                    ret.Add(argumentParser.GetDefault());
                }
            }

            return ret;
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
        static CommandArgumentConverter Converter = new CommandArgumentConverter();

        public CommandArgumentInfo ArgumentInfo { get; set; }

        public SingleArgumentParser(ParameterInfo parameter)
        {
            this.ArgumentInfo = new CommandArgumentInfo(parameter);
        }

        public object Parse(string v)
        {
            if (ArgumentInfo.HasConverter())
            {
                return ArgumentInfo.GetConverter().Convert(v);
            }
            else
            {
                return Converter.Convert(ArgumentInfo.ValueType, v);
            }
        }

        public object GetDefault()
        {
            switch (ArgumentInfo.ArgumentType)
            {
                case CommandArgumentType.Positional:
                    throw new LackPositionalArgumentException("lack argument: " + ArgumentInfo.GetPositionalArgumentExpression());
                case CommandArgumentType.Keyword:
                    return ArgumentInfo.GetDefault();
                case CommandArgumentType.Flag:
                    return false;
                case CommandArgumentType.Variable:
                    return ArgumentInfo.ValueType.GetConstructor(new Type[] { typeof(Int32) }).Invoke(new object[] { 0 });
                default:
                    throw new Exception("unexpected state");
            }
        }
    }
}