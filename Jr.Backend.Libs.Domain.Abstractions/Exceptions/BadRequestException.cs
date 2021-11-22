using System;
using System.Runtime.Serialization;

namespace Jr.Backend.Libs.Domain.Abstractions.Exceptions
{
    /// <summary>
    /// Dispara uma <see cref="Exception"/> do tipo <see cref="BadRequestException"/>.
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}