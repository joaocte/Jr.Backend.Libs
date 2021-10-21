namespace Jr.Backend.Libs.Messaging.Abstractions.Interfaces
{
    public interface IMessageFormatter<TypeMessage, TTMessageOut> where TypeMessage : MessageType
    {
        TTMessageOut Format(TypeMessage message);
    }
}