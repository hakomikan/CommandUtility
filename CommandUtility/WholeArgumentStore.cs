using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility
{
    public class WholeArgumentStore
    {
        private CommandClassInfo commandClassInfo;

        public Dictionary<CommandParameterInfo, IArgumentStore> ArgumentStores = new Dictionary<CommandParameterInfo, IArgumentStore>();

        public WholeArgumentStore(CommandClassInfo commandClassInfo)
        {
            this.commandClassInfo = commandClassInfo;
        }

        public object[] FunctionArguments
        {
            get
            {
                return (from parameter
                        in commandClassInfo.MainCommand.Parameters
                        select GetParsedArgument(parameter)
                        ).ToArray();
            }
        }

        private object GetParsedArgument(CommandParameterInfo parameter)
        {
            if (ArgumentStores.ContainsKey(parameter))
            {
                return ArgumentStores[parameter].Get();
            }
            else
            {
                return parameter.GetDefault();
            }
        }

        public string[] RestArguments { get; set; }

        public void StoreValue(CommandParameterInfo parameterInfo, string value)
        {
            if (!ArgumentStores.ContainsKey(parameterInfo))
            {
                ArgumentStores[parameterInfo] = parameterInfo.CreateArgumentStore();
            }

            ArgumentStores[parameterInfo].Store(parameterInfo.Convert(value));
        }

        public void StoreFlag(CommandParameterInfo parameterInfo, bool flag = true)
        {
            if (!ArgumentStores.ContainsKey(parameterInfo))
            {
                ArgumentStores[parameterInfo] = parameterInfo.CreateArgumentStore();
            }

            ArgumentStores[parameterInfo].Store(flag);
        }
    }
}
