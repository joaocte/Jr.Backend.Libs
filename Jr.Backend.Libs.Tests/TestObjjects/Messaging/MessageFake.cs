namespace Jr.Backend.Libs.Tests.TestObjjects.Messaging
{
    public class MessageFake : IEvent
    {
        public string MessageName { get; set; }
        public int Code { get; set; }
    }
}