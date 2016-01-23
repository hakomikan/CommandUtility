using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandUtility
{
    public class CommandLineParser
    {
        public enum ArgumentType
        {
            Flag,
            Keyword,
            Value
        }

        public class ClassifiedArgument
        {
            public ClassifiedArgument(ArgumentType argumentType, CommandParameterInfo parameterInfo, string value)
            {
                this.ArgumentType = argumentType;
                this.ParameterInfo = parameterInfo;
                this.Value = value;
            }

            public ArgumentType ArgumentType { get; private set; }
            public CommandParameterInfo ParameterInfo { get; private set; }
            public string Value { get; private set; }
        }

        public CommandClassInfo CommandClassInfo { get; private set; }

        public object[] ParseAsFunctionArguments(string[] arguments)
        {
            return Parse(arguments).FunctionArguments.ToArray();
        }

        public CommandLineParser(Type type)
        {
            this.CommandClassInfo = new CommandClassInfo(type);
        }

        public WholeArgumentStore ParseV(params string[] arguments)
        {
            return Parse(arguments);
        }

        public WholeArgumentStore Parse(string[] arguments)
        {
            var result = new WholeArgumentStore(CommandClassInfo);

            var classifiedArguments = from argument in arguments select ClassifyArgument(argument);

            var restArguments1 = ParseKeywordArguments(result, classifiedArguments);
            var restArguments2 = ParseFlagArguments(result, restArguments1);
            var restArguments3 = ParseSequentialArguments(result, restArguments2);

            result.RestArguments = (from argument in restArguments3 select argument.Value).ToArray();

            if(0 < result.RestArguments.Count())
            {
                throw new TooManyArgumentException("Too many arguments: " + string.Join(",", result.RestArguments));
            }

            return result;
        }

        private IEnumerable<ClassifiedArgument> ParseKeywordArguments(WholeArgumentStore result, IEnumerable<ClassifiedArgument> arguments)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var keywordOption = enumerator.Current;

                if (keywordOption.ArgumentType == ArgumentType.Keyword)
                {
                    if (enumerator.MoveNext())
                    {
                        var value = enumerator.Current;
                        if (value.ArgumentType == ArgumentType.Value)
                        {
                            result.StoreValue(keywordOption.ParameterInfo, value.Value);
                        }
                        else
                        {
                            throw new LackKeywordArgumentValueException("lack value after: " + keywordOption);
                        }
                    }
                    else
                    {
                        throw new LackKeywordArgumentValueException("lack value after: " + keywordOption);
                    }
                }
                else
                {
                    yield return keywordOption;
                }
            }
        }

        private IEnumerable<ClassifiedArgument> ParseFlagArguments(WholeArgumentStore result, IEnumerable<ClassifiedArgument> arguments)
        {
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var flagOption = enumerator.Current;

                if (flagOption.ArgumentType == ArgumentType.Flag)
                {
                    result.StoreFlag(flagOption.ParameterInfo, true);
                }
                else
                {
                    yield return flagOption;
                }
            }
        }

        private IEnumerable<ClassifiedArgument> ParseSequentialArguments(WholeArgumentStore result, IEnumerable<ClassifiedArgument> arguments)
        {
            var parameterEnumerator = this.CommandClassInfo.MainCommand.Parameters.GetEnumerator();
            var argumentEnumerator = arguments.GetEnumerator();
            while (parameterEnumerator.MoveNext() && argumentEnumerator.MoveNext())
            {
                var parameterInfo = parameterEnumerator.Current;
                var argument = argumentEnumerator.Current;

                if (argument.ArgumentType == ArgumentType.Value)
                {
                    result.StoreValue(parameterInfo, argument.Value);
                }
                else
                {
                    throw new UnknownOptionException("Unknown option: " + argument.Value);
                }

                if (parameterInfo.IsMultiValued)
                {
                    while (argumentEnumerator.MoveNext())
                    {
                        result.StoreValue(parameterInfo, argumentEnumerator.Current.Value);
                    }
                }
            }

            while (argumentEnumerator.MoveNext())
            {
                yield return argumentEnumerator.Current;
            }
        }

        public ClassifiedArgument ClassifyArgument(string argument)
        {
            if (CommandExpressionUtility.IsOptionExpression(argument))
            {
                var keywordArgument = MatchKeywordArgument(argument);
                if (keywordArgument != null)
                {
                    return new ClassifiedArgument(ArgumentType.Keyword, keywordArgument, argument);
                }

                var flagArgument = MatchFlagArgument(argument);
                if (flagArgument != null)
                {
                    return new ClassifiedArgument(ArgumentType.Flag, flagArgument, argument);
                }

                throw new UnknownOptionException("Unknown option: " + argument);
            }
            else
            {
                return new ClassifiedArgument(ArgumentType.Value, null, argument);
            }
        }

        public CommandParameterInfo MatchKeywordArgument(string v)
        {
            foreach (var parameter in this.CommandClassInfo.Parameters)
            {
                if (parameter.IsKeywordArgument && parameter.GetOptionExpression() == v)
                {
                    return parameter;
                }
            }
            return null;
        }

        public CommandParameterInfo MatchFlagArgument(string v)
        {
            foreach (var parameter in this.CommandClassInfo.Parameters)
            {
                if (parameter.IsFlagArgument && parameter.GetOptionExpression() == v)
                {
                    return parameter;
                }
            }
            return null;
        }
    }
}
