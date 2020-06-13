using System;
using System.Runtime.Serialization;

namespace Mycroft.EntityFrameworkCore.Core.Utility.Exceptions
{
    [Serializable]
    public class NotActiveException : Exception
    {
        public NotActiveException()
        {
        }

        public NotActiveException(string message) : base(message)
        {
        }

        public NotActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}