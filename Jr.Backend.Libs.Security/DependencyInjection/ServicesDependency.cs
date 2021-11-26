using Jr.Backend.Libs.Security.Abstractions;
using Jr.Backend.Libs.Security.Abstractions.Application;
using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Jr.Backend.Libs.Security.Application;
using Jr.Backend.Libs.Security.Context;
using Jr.Backend.Libs.Security.Infrastructure.Repository.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Jr.Backend.Libs.Security.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrSecurityApi(this IServiceCollection services)
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

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer {token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void AddServiceDependencyJrSecurityApiUsingCustomValidate(this IServiceCollection services, Func<ISecurityConfiguration> configuration)
        {
            services.AddScoped<IMongoContextSecurity>((_) =>
            {
                var securityConfiguration = configuration();
                var config = new ConfigurationBuilder().AddInMemoryCollection(securityConfiguration.InMemoryCollection).Build();

                return new MongoContextSecurity(config);
            });
            services.AddScoped<ITenantRepositorySecurity, TenantRepositorySecurity>();
            services.AddScoped<IValidateToken, ValidateToken>();
            AddServiceDependencyJrSecurityApi(services);
        }
    }
}