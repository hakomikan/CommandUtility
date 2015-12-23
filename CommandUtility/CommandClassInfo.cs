using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandUtility
{
    public class CommandParameterInfo
    {
        public ParameterInfo ParameterInfo { get; private set; }

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

        public CommandParameterInfo(ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;
        }
    }

    public class CommandMethodInfo
    {
        public MethodInfo MethodInfo { get; private set; }

        public CommandMethodInfo(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }

        public IEnumerable<CommandParameterInfo> Parameters
        {
            get
            {
                return from parameter in MethodInfo.GetParameters() select new CommandParameterInfo(parameter);
            }
        }
    }

    public class CommandClassInfo
    {
        private Type type;

        public CommandClassInfo(Type type)
        {
            this.type = type;
        }

        public bool HasMainCommand
        {
            get
            {
                return type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public) != null;
            }
        }

        public IEnumerable<CommandMethodInfo> MainCommands
        {
            get
            {
                return from method
                       in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                       where method.Name == "Main"
                       select new CommandMethodInfo(method);
            }
        }

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
