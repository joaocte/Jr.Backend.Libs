using Jr.Backend.Libs.Domain.Abstractions.Notifications;

namespace Jr.Backend.Libs.Domain.Notifications
{
    public class Notification : NotificationAbstract
    {
        public Notification(string key, string message) : base(key, message)
        {
        }
    }
}