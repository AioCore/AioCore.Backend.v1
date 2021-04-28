using System;

namespace AioCore.Shared.Exceptions
{
    public class AioCoreException : Exception
    {
        public AioCoreException()
        { }

        public AioCoreException(string message)
            : base(message)
        { }

        public AioCoreException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}