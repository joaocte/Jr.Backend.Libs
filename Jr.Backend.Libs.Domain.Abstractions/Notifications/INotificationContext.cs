using FluentValidation.Results;
using System.Collections.Generic;

namespace Jr.Backend.Libs.Domain.Abstractions.Notifications
{
    public interface INotificationContext
    {
        void AddNotification(string key, string message);

        void AddNotification(NotificationAbstract notification);

        void AddNotifications(IReadOnlyCollection<NotificationAbstract> notifications);

        void AddNotifications(IList<NotificationAbstract> notifications);

        void AddNotifications(ICollection<NotificationAbstract> notifications);

        void AddNotifications(ValidationResult validationResult);

        bool HasNotifications();

        public IReadOnlyCollection<NotificationAbstract> GetNotifications();
    }
}