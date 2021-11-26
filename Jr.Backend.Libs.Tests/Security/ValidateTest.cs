using Jr.Backend.Libs.Security.Abstractions.Application;
using Jr.Backend.Libs.Security.Abstractions.Entity;
using Jr.Backend.Libs.Security.Application;
using NSubstitute;
using System;
using System.Linq.Expressions;
using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Xunit;

namespace Jr.Backend.Libs.Tests.Security
{
    public class ValidateTest
    {
        private readonly IValidateToken validateToken;
        private readonly ITenantRepositorySecurity tenantRepository;

        public ValidateTest()
        {
            tenantRepository = Substitute.For<ITenantRepositorySecurity>();
            validateToken = new ValidateToken(tenantRepository);
        }

        [Fact]
        public void AoReceberUmTokenValidoComBearer_ReturnTrue()
        {
            var tenant = new Tenant
            {
                ClientId = Guid.NewGuid(),
                ClientSecret = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Status = "Ativo",
                TenantKey = "TenantKey",
                TenantName = "TenantName"
            };
            var token =
                "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUZW5hbnROYW1lIjoic3RyaW5nIiwiQ2xpZW50SWQiOiIzNjljNzZlNi0wMzJiLTRjNWMtOTU2NS05ZGIyNGM1NDU0NzgiLCJDbGllbnRTZWNyZXQiOiJkYWE3NGE5Ny04MjUxLTQ5NjgtODA0Zi02NWYxNjBjYmU5M2MiLCJUZW5hbnRLZXkiOiJzdHJpbmciLCJleHAiOjE2Mzc1ODIyNzV9.fJL7m8lYf5uh8OgU9ZSrafNXm5A5pT7gpQdp_-XyVfE";
            tenantRepository.GetAsync(Arg.Any<Expression<Func<Tenant, bool>>>()).Returns(tenant);
            Assert.True(validateToken.ExecuteAsync(token).Result);
        }

        [Fact]
        public void AoReceberUmTokenValidoSemBearer_ReturnTrue()
        {
            var tenant = new Tenant
            {
                ClientId = Guid.NewGuid(),
                ClientSecret = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Status = "Ativo",
                TenantKey = "TenantKey",
                TenantName = "TenantName"
            };
            var token =
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUZW5hbnROYW1lIjoic3RyaW5nIiwiQ2xpZW50SWQiOiIzNjljNzZlNi0wMzJiLTRjNWMtOTU2NS05ZGIyNGM1NDU0NzgiLCJDbGllbnRTZWNyZXQiOiJkYWE3NGE5Ny04MjUxLTQ5NjgtODA0Zi02NWYxNjBjYmU5M2MiLCJUZW5hbnRLZXkiOiJzdHJpbmciLCJleHAiOjE2Mzc1ODIyNzV9.fJL7m8lYf5uh8OgU9ZSrafNXm5A5pT7gpQdp_-XyVfE";
            tenantRepository.GetAsync(Arg.Any<Expression<Func<Tenant, bool>>>()).Returns(tenant);
            Assert.True(validateToken.ExecuteAsync(token).Result);
        }
    }
}