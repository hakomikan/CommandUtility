using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility
{
    public class CommandInvoker<T> where T : class, new()
    {
        public CommandInvoker(T instance = default(T))
        {
            if(instance == default(T))
            {
                Instance = new T();
            }
            else
            {
                Instance = instance;
            }
        }

        public T Instance { get; set; }

        public int Invoke(object[] arguments)
        {
            return (int)Instance.GetType().GetMethod("Main", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Invoke(Instance, arguments);
        }
    }
}
