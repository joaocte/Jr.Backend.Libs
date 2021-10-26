using Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository;
using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions;
using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jr.Backend.Libs.Infrastructure.MongoDB.Context;
using Jr.Backend.Libs.Infrastructure.MongoDB.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Jr.Backend.Libs.Infrastructure.MongoDB.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrInfrastructureMongoDb(this IServiceCollection services, ConnectionType connectionType = ConnectionType.ReplicaSet)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();
            services.AddScoped<IMongoContext>((_) =>
            {
                var config = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                 .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                                 .AddEnvironmentVariables()
                                 .Build();

                return new MongoContext(config, connectionType);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}