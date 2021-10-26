using System;
using System.Runtime.Serialization;

namespace Jr.Backend.Libs.Domain.Abstractions.Interfaces.Exceptions
{
    /// <summary>
    /// Dispara uma <see cref="Exception"/> do tipo <see cref="DomainException"/>.
    /// </summary>
    public abstract class DomainException : Exception
    {
        protected DomainException()
        {
        }

        protected DomainException(string message) : base(message)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}