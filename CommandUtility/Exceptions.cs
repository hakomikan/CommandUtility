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
    public class LackArgumentException : Exception
    {
        public LackArgumentException() { }
        public LackArgumentException(string message) : base(message) { }
        public LackArgumentException(string message, Exception inner) : base(message, inner) { }
        protected LackArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class TooManyArgumentException : Exception
    {
        public TooManyArgumentException() { }
        public TooManyArgumentException(string message) : base(message) { }
        public TooManyArgumentException(string message, Exception inner) : base(message, inner) { }
        protected TooManyArgumentException(
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


    [Serializable]
    public class LackKeywordArgumentValueException : Exception
    {
        public LackKeywordArgumentValueException() { }
        public LackKeywordArgumentValueException(string message) : base(message) { }
        public LackKeywordArgumentValueException(string message, Exception inner) : base(message, inner) { }
        protected LackKeywordArgumentValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


    [Serializable]
    public class HasNoMainCommandException : Exception
    {
        public HasNoMainCommandException() { }
        public HasNoMainCommandException(string message) : base(message) { }
        public HasNoMainCommandException(string message, Exception inner) : base(message, inner) { }
        protected HasNoMainCommandException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
