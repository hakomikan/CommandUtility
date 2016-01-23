using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility
{
    public class CommandBase<T> where T : class, new()
    {
        public static void Main(string[] args)
        {
            Environment.ExitCode = new CommandInterface<T>().Run(args.Skip(1).ToArray());
        }

        public int Invoke(string[] args)
        {
            return new CommandInterface<T>().Run(args);
        }
    }
}
