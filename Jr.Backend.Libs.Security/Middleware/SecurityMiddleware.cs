using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
using Jr.Backend.Libs.Security.Abstractions.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Security.Middleware
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using IValidateToken validateToken = context.RequestServices.GetService<IValidateToken>();

            string token = context.Request.Headers["Authorization"];

            if (token == null || validateToken == null || !await validateToken.ExecuteAsync(token))
                throw new UnauthorizedException("Access denied!");

            await next(context);
        }
    }
}