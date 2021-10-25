using Jr.Backend.Libs.Messaging.Abstractions.Interfaces;
using System;
using System.Text.Json;

namespace Jr.Backend.Libs.Messaging.Abstractions
{
    public abstract class MessageFormatterBase<TypeMessage> : IMessageFormatter<TypeMessage, string> where TypeMessage : IIntegrationEvent
    {
        /// <summary>
        /// Convert the <paramref name="message"/> to json.
        /// </summary>
        /// <param name="message"></param>
        /// <returns><paramref name="message"/> in json format</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        public string Format(TypeMessage message)
        {
            return message == null
                ? throw new ArgumentNullException("The message cannot be null")
                : JsonSerializer.Serialize(message);
        }
    }
}