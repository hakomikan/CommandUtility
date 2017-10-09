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

        public ConfigType Load<ConfigType>(string configText)
        {
            return default(ConfigType);
        }

        public ConfigType Load<ConfigType>(FileInfo configFile)
        {
            return default(ConfigType);
        }
    }
}
