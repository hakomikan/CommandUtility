using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface
{
    public class ConfigLoader
    {
        public ConfigLoader()
        {

        }

        public ConfigType Load<ConfigType>(string configText) where ConfigType: class, new()
        {
            var assembly = Utility.CSharpAssembly.Compile(configText);

            foreach (var type in assembly.DefinedTypes)
            {
                if(type.AsType() == typeof(ConfigType) || type.IsSubclassOf(typeof(ConfigType)))
                {
                    var constructor = type.GetConstructor(new Type[] { });

                    return constructor.Invoke(new object[] { }) as ConfigType;
                }
            }

            return default(ConfigType);
        }

        public ConfigType Load<ConfigType>(FileInfo configFile)
        {
            return default(ConfigType);
        }
    }
}
