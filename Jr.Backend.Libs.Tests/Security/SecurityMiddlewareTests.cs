using Jr.Backend.Libs.Security.Abstractions.Application;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Jr.Backend.Libs.Tests.Security
{
    public class SecurityMiddlewareTests
    {
        private readonly RequestDelegate requestDelegate;
        private readonly HttpContext context;

        //private readonly SecurityMiddleware securityMiddleware;
        private readonly IValidateToken validateToken;

        public SecurityMiddlewareTests()
        {
            context = Substitute.For<HttpContext>();
            validateToken = Substitute.For<IValidateToken>();
            //securityMiddleware = new SecurityMiddleware(requestDelegate);
        }

        //[Fact]
        //public void TesteSecurityMiddleware()
        //{
        //    context.RequestServices.GetService<IValidateToken>().Returns(validateToken);

        //    context.Request.Headers["Authorization"].Returns(@"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUZW5hbnROYW1lIjoic3RyaW5nIiwiQ2xpZW50SWQiOiIzNjljNzZlNi0wMzJiLTRjNWMtOTU2NS05ZGIyNGM1NDU0NzgiLCJDbGllbnRTZWNyZXQiOiJkYWE3NGE5Ny04MjUxLTQ5NjgtODA0Zi02NWYxNjBjYmU5M2MiLCJUZW5hbnRLZXkiOiJzdHJpbmciLCJleHAiOjE2Mzc1ODIyNzV9.fJL7m8lYf5uh8OgU9ZSrafNXm5A5pT7gpQdp_-XyVfE");

        //    securityMiddleware.InvokeAsync(context).Wait();
        //}
    }
}