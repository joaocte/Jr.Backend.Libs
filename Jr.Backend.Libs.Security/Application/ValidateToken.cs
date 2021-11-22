using Jr.Backend.Libs.Domain.Abstractions.Exceptions;
using Jr.Backend.Libs.Security.Abstractions;
using Jr.Backend.Libs.Security.Abstractions.Application;
using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Security.Application
{
    public class ValidateToken : IValidateToken
    {
        private readonly ITenantRepository tenantRepository;

        public ValidateToken(ITenantRepository tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public void Dispose()
        {
            tenantRepository.Dispose();
        }

        public async Task<bool> ExecuteAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var clientId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "ClientId")?.Value;
            var clientSecret = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "ClientSecret")?.Value;
            var tenantName = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "TenantName")?.Value;
            var tenantKey = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "TenantKey")?.Value;

            var clientIdGuid = string.IsNullOrWhiteSpace(clientId) ? Guid.Empty : new Guid(clientId);
            var clientSecretGuid = string.IsNullOrWhiteSpace(clientSecret) ? Guid.Empty : new Guid(clientSecret);

            var tenant = await tenantRepository.GetAsync(x =>
                x.ClientId == clientIdGuid && x.ClientSecret == clientSecretGuid);

            if (tenant == null)
                throw new BadRequestException("Token Informado é Iválido");

            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            var principal = handler.ValidateToken(token, validationParameters, out validatedToken);
            return true;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = null,
                ValidAudience = null,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.PrivateKey)) // The same key as the one that generate the token
            };
        }
    }
}