using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions;
using Jr.Backend.Libs.Security.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Jr.Backend.Libs.Security.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrSecurityApi(this IServiceCollection services, ConnectionType connectionType = ConnectionType.ReplicaSet)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = Constants.TokenValidationParameters;
                });
        }

        //public static void AddServiceDependencyJrSecurityAuth(this IServiceCollection services, ConnectionType connectionType = ConnectionType.ReplicaSet)
        //{
        //    services.AddScoped<IMongoContext>((p) =>
        //    {
        //        var config = new ConfigurationBuilder()
        //            .AddInMemoryCollection(SecurityConfiguration.InMemoryDesCollection)
        //            .Build();

        //        return new MongoContext(config, connectionType);
        //    });
        //    services.AddScoped<ITenantRepository>(p =>
        //    {
        //        var mongoContext = p.GetService<IMongoContext>();
        //        return new TenantRepository(mongoContext, nameof(Tenant));
        //    });

        //    services.AddServiceDependencyJrFrameworkCustomExceptionFilter();
        //    AddServiceDependencyJrSecurityApi(services);
        //    services.AddScoped<IValidateToken, ValidateToken>();
        //}
    }
}