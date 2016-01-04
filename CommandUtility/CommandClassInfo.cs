using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandUtility
{
    public enum CommandArgumentType
    {
        Positional,
        Keyword,
        Flag,
        Variable
    }

    public class CommandParameterInfo : IEqualityComparer<CommandParameterInfo>
    {
        public static CommandArgumentConverter Converter = new CommandArgumentConverter();
        public ParameterInfo ParameterInfo { get; private set; }

        public string Name
        {
            get
            {
                return ParameterInfo.Name;
            }
        }

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

        public bool IsListParameter
        {
            get
            {
                return 
                    ParameterInfo.ParameterType.IsConstructedGenericType && 
                    ParameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(List<>);
            }
        }

        public bool IsArrayParameter
        {
            get
            {
                return ParameterInfo.ParameterType.IsArray;
            }
        }

        public bool IsMultiValued
        {
            get
            {
                return IsArrayParameter || IsListParameter;
            }
        }

        public Type ParameterType
        {
            get
            {
                if(IsArrayParameter)
                {
                    return ParameterInfo.ParameterType.GetElementType();
                }
                else if(IsListParameter)
                {
                    return ParameterInfo.ParameterType.GenericTypeArguments[0];
                }
                else
                {
                    return ParameterInfo.ParameterType;
                }
            }
        }

        public bool IsFlagArgument
        {
            get
            {
                return ParameterInfo.ParameterType == typeof(bool);
            }
        }

        public bool IsKeywordArgument
        {
            get
            {
                return ParameterInfo.HasDefaultValue && ParameterInfo.ParameterType != typeof(bool);
            }
        }

        public bool IsPositionalArgument
        {
            get
            {
                return !ParameterInfo.HasDefaultValue && ParameterInfo.ParameterType != typeof(bool) && !IsVariableArgument;
            }
        }

        public bool IsVariableArgument
        {
            get
            {
                return ParameterInfo.GetCustomAttributes<ParamArrayAttribute>().Count() > 0;
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
                return ParameterInfo.HasDefaultValue || IsFlagArgument;
            }
        }

        public CommandParameterInfo(ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;
        }

        public string GetOptionExpression()
        {
            return "--" + Regex.Replace(ParameterInfo.Name, "[A-Z]", match => "-" + match.Captures[0].Value.ToLower());
        }

        public bool HasConverter()
        {
            return ParameterInfo.GetCustomAttribute<ICommandArgumentConverterAttribute>(true) != null;
        }

        public ICommandArgumentConverter GetConverter()
        {
            return ParameterInfo.GetCustomAttribute<ICommandArgumentConverterAttribute>(true);
        }

        public object Convert(string v)
        {
            if (HasConverter())
            {
                return GetConverter().Convert(v);
            }
            else
            {
                return Converter.Convert(ParameterType, v);
            }
        }

        public object GetDefault()
        {
            if (ParameterInfo.HasDefaultValue)
            {
                return ParameterInfo.DefaultValue;
            }
            else if(IsFlagArgument)
            {
                return false;
            }
            else
            {
                throw new LackArgumentException("Lack argument: name = " + ParameterInfo.Name);
            }
        }

        public bool Equals(CommandParameterInfo x, CommandParameterInfo y)
        {
            return x.ParameterInfo == y.ParameterInfo;
        }

        public int GetHashCode(CommandParameterInfo obj)
        {
            return ParameterInfo.GetHashCode();
        }

        public IArgumentStore CreateArgumentStore()
        {
            if (IsArrayParameter)
            {
                return (IArgumentStore)(typeof(ArrayArgumentStore<>).MakeGenericType(ParameterType).GetConstructor(new Type[] { }).Invoke(new object[] { }));
            }
            else if (IsListParameter)
            {
                return (IArgumentStore)(typeof(ListArgumentStore<>).MakeGenericType(ParameterType).GetConstructor(new Type[] { }).Invoke(new object[] { }));
            }
            else
            {
                return new SingleArgumentStore();
            }
        }
    }

    public class CommandMethodInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IEnumerable<CommandParameterInfo> Parameters { get; private set; }

        public CommandMethodInfo(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            Parameters = (from parameter in MethodInfo.GetParameters() select new CommandParameterInfo(parameter)).ToList();
        }
    }

    public class CommandClassInfo
    {
        private Type type;

        public CommandClassInfo(Type type)
        {
            this.type = type;
            MainCommands = (from method
                           in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                           where method.Name == "Main"
                           select new CommandMethodInfo(method)).ToList();
        }

        public bool HasMainCommand
        {
            get
            {
                return type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public) != null;
            }
        }

        public IEnumerable<CommandMethodInfo> MainCommands { get; private set; }

        public CommandMethodInfo MainCommand
        {
            get
            {
                var ret = MainCommands.FirstOrDefault();

                if (ret == null)
                {
                    throw new HasNoMainCommandException();
                }

                return ret;
            }
        }
    }
}
