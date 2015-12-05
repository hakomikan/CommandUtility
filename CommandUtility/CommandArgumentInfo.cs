using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandUtility
{
    public enum CommandArgumentType
    {
        Positional,
        Keyword,
        Flag
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
                if (parameter.ParameterType == typeof(bool))
                {
                    return CommandArgumentType.Flag;
                }
                else if (parameter.HasDefaultValue)
                {
                    return CommandArgumentType.Keyword;
                }
                else
                {
                    return CommandArgumentType.Positional;
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
                return !parameter.HasDefaultValue && parameter.ParameterType != typeof(bool);
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
    }
}
