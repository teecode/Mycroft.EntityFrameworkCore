using System;
using System.Runtime.Serialization;

namespace Mycroft.EntityFrameworkCore.Core.Utility.Exceptions
{
    [Serializable]
    public class NotVerifiedException : Exception
    {
        public NotVerifiedException()
        {
        }

        public NotVerifiedException(string message) : base(message)
        {
        }

        public NotVerifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotVerifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}