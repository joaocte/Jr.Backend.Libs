using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Jr.Backend.Libs.Security.DependencyInjection;
using Jr.Backend.Libs.Security.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Jr.Backend.Libs.Tests.Security
{
    public class ServicesDependencyTests
    {
        [Fact]
        public void DeveRegistrarTodasDependencias()
        {
            var serviceCollection = new ServiceCollection();
            Func<ISecurityConfiguration> options = () => new SecurityConfiguration("mongodb://localhost:27017/?authSource=admin", "JrTenant");
            serviceCollection.AddServiceDependencyJrSecurityApiUsingCustomValidate(options);

            var provider = serviceCollection.BuildServiceProvider();

            var service = provider.GetService<ITenantRepositorySecurity>();

            var result = service.GetAllAsync().Result;
        }
    }
}