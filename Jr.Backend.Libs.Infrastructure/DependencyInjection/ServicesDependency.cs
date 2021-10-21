using Jr.Backend.Libs.Infrastructure.Abstractions.Interfaces;
using Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Jr.Backend.Libs.Infrastructure.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrInfrastructure(this IServiceCollection services)
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

                return new MongoContext(config);
            });
        }
    }
}