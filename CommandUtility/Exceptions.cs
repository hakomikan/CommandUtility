using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUtility
{
    [Serializable]
    public class CommandArgumentException : Exception
    {
        public CommandArgumentException() { }
        public CommandArgumentException(string message) : base(message) { }
        public CommandArgumentException(string message, Exception inner) : base(message, inner) { }
        protected CommandArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class LackPositionalArgumentException : Exception
    {
        public LackPositionalArgumentException() { }
        public LackPositionalArgumentException(string message) : base(message) { }
        public LackPositionalArgumentException(string message, Exception inner) : base(message, inner) { }
        protected LackPositionalArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


    [Serializable]
    public class TooManyPositionalArgumentException : Exception
    {
        public TooManyPositionalArgumentException() { }
        public TooManyPositionalArgumentException(string message) : base(message) { }
        public TooManyPositionalArgumentException(string message, Exception inner) : base(message, inner) { }
        protected TooManyPositionalArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


    [Serializable]
    public class InvalidTypeArgumentException : Exception
    {
        public InvalidTypeArgumentException() { }
        public InvalidTypeArgumentException(string message) : base(message) { }
        public InvalidTypeArgumentException(string message, Exception inner) : base(message, inner) { }
        protected InvalidTypeArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
