using System;

namespace Jr.Backend.Libs.Messaging.Abstractions
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime PublicatedDate { get; }
        public string PublicatedTime { get; }

        protected IntegrationEvent()
        {
            var UtcDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
            PublicatedDate = UtcDate.Date;
            PublicatedTime = UtcDate.ToString("T");
        }
    }
}