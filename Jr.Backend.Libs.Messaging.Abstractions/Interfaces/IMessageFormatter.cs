namespace Jr.Backend.Libs.Messaging.Abstractions.Interfaces
{
    public interface IMessageFormatter<TypeMessage, TTMessageOut> where TypeMessage : IntegrationEvent
    {
        TTMessageOut Format(TypeMessage message);
    }
}