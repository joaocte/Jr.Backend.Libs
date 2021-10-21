using System;

namespace Jr.Backend.Libs.Messaging.Abstractions
{
    public abstract class MessageType
    {
        public Guid Id { get; }
        public DateTime PublicatedDateUtc { get; }
        public string PublicatedTime { get; }

        protected MessageType()
        {
            var UtcDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
            PublicatedDateUtc = UtcDate.Date;
            PublicatedTime = UtcDate.ToString("T");
        }
    }
}