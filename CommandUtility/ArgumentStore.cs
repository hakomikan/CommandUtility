using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility
{
    public interface IArgumentStore
    {
        bool HasValue { get; }
        void Store(object argument);
        object Get();
    }

    public class SingleArgumentStore : IArgumentStore
    {
        bool IsStored = false;
        object Value;

        public bool HasValue
        {
            get
            {
                return IsStored;
            }
        }

        public object Get()
        {
            if (IsStored)
            {
                return Value;
            }
            else
            {
                throw new LackArgumentException();
            }
        }

        public void Store(object argument)
        {
            if (IsStored)
            {
                throw new TooManyArgumentException();
            }
            else
            {
                Value = argument;
                IsStored = true;
            }
        }
    }

    public class ListArgumentStore<T> : IArgumentStore
    {
        List<T> Values;

        public ListArgumentStore()
        {
            Values = new List<T>();
        }

        public bool HasValue
        {
            get
            {
                return 0 < Values.Count;
            }
        }

        public object Get()
        {
            return Values;
        }

        public void Store(object argument)
        {
            Values.Add((T)argument);
        }
    }

    public class ArrayArgumentStore<T> : IArgumentStore
    {
        List<T> Values;

        public ArrayArgumentStore()
        {
            Values = new List<T>();
        }

        public bool HasValue
        {
            get
            {
                return 0 < Values.Count;
            }
        }

        public object Get()
        {
            return Values.ToArray();
        }

        public void Store(object argument)
        {
            Values.Add((T)argument);
        }
    }
}
