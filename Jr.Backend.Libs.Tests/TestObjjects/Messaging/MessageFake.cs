using Jr.Backend.Libs.Messaging.Abstractions;

namespace Jr.Backend.Libs.Tests.TestObjjects.Messaging
{
    public class MessageFake : Event
    {
        public string MessageName { get; set; }
        public int Code { get; set; }
    }
}