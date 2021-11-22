using Jr.Backend.Libs.Framework.DependencyInjection;
using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions;
using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jr.Backend.Libs.Infrastructure.MongoDB.DependencyInjection;
using Jr.Backend.Libs.Security.Abstractions.Application;
using Jr.Backend.Libs.Security.Abstractions.Entity;
using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interface;
using Jr.Backend.Libs.Security.Application;
using Jr.Backend.Libs.Security.Infrastructure.Repository.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Jr.Backend.Libs.Security.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencySecurity(this IServiceCollection services)
        {
            services.AddServiceDependencyJrInfrastructureMongoDb(ConnectionType.DirectConnection);
            services.AddScoped<ITenantRepository>(p =>
            {
                var mongoContext = p.GetService<IMongoContext>();
                return new TenantRepository(mongoContext, nameof(Tenant));
            });
            services.AddServiceDependencyJrFrameworkCustomExceptionFilter();

            services.AddTransient<IValidateToken, ValidateToken>();
        }
    }
}