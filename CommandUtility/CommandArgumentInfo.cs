using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;

namespace CommandUtility
{
    public enum CommandArgumentType
    {
        Positional,
        Keyword,
        Flag,
        Variable
    }

    public class CommandArgumentInfo
    {
        private ParameterInfo parameter;

        public CommandArgumentInfo(ParameterInfo parameter)
        {
            this.parameter = parameter;
            this.Name = parameter.Name;
        }

        public Type ValueType { get { return parameter.ParameterType; } }

        public CommandArgumentType ArgumentType
        {
            get
            {
                if (IsFlagArgument)
                {
                    return CommandArgumentType.Flag;
                }
                else if (IsKeywordArgument)
                {
                    return CommandArgumentType.Keyword;
                }
                else if (IsPositionalArgument)
                {
                    return CommandArgumentType.Positional;
                }
                else if (IsVariableArgument)
                {
                    return CommandArgumentType.Variable;
                }
                else
                {
                    throw new Exception("Unknown Type Argument");
                }
            }
        }

        public bool IsFlagArgument
        {
            get
            {
                return parameter.ParameterType == typeof(bool);
            }
        }

        public bool IsKeywordArgument
        {
            get
            {
                return parameter.HasDefaultValue && parameter.ParameterType != typeof(bool);
            }
        }

        public bool IsPositionalArgument
        {
            get
            {
                return !parameter.HasDefaultValue && parameter.ParameterType != typeof(bool) && !IsVariableArgument;
            }
        }

        public bool IsVariableArgument
        {
            get
            {
                return parameter.GetCustomAttributes<ParamArrayAttribute>().Count() > 0;
            }
        }

        public bool IsSequentialArgument
        {
            get
            {
                return IsPositionalArgument || IsVariableArgument;
            }
        }

        public bool IsMultiple
        {
            get
            {
                return IsVariableArgument;
            }
        }

        public bool IsRequired
        {
            get
            {
                return !IsOptional;
            }
        }

        public bool IsOptional
        {
            get
            {
                return parameter.HasDefaultValue || IsFlagArgument;
            }
        }

        public string Name { get; set; }

        public string GetOptionExpression()
        {
            return "--" + Regex.Replace(Name, "[A-Z]", match => "-" + match.Captures[0].Value.ToLower());
        }

        public string GetPositionalArgumentExpression()
        {
            return string.Format("<{0}>", Name);
        }

        public object GetDefault()
        {
            if(parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }
            else
            {
                throw new LackArgumentException("Lack argument: name = " + Name);
            }
        }

        public bool HasConverter()
        {
            return parameter.GetCustomAttribute<ICommandArgumentConverterAttribute>(true) != null;
        }

        public ICommandArgumentConverter GetConverter()
        {
            return parameter.GetCustomAttribute<ICommandArgumentConverterAttribute>(true);
        }
    }
}
