using Jr.Backend.Libs.Domain.Abstractions.Notifications;
using Jr.Backend.Libs.Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Jr.Backend.Libs.Framework.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrFramework(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddMvcCore(options =>
            {
                options.Filters.Add<NotificationFilter>();
                options.Filters.Add<CustomExceptionFilter>();
            });
        }
    }
}