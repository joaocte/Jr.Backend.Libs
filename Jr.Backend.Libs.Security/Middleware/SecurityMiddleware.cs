using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
using Jr.Backend.Libs.Security.Abstractions.Application;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Security.Middleware
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IValidateToken validateToken;

        public SecurityMiddleware(RequestDelegate next, IValidateToken validateToken)
        {
            this.next = next;
            this.validateToken = validateToken;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];
            if (token == null)
            {
                throw new UnauthorizedException("Access denied!");
            }

            if (await validateToken.ExecuteAsync(token))
                await next(context);
        }
    }
}