using Jr.Backend.Libs.API.Abstractions;
using Jr.Backend.Libs.API.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Jr.Backend.Libs.API.DependencyInjection
{
    public static class ServicesDependency
    {
        private static IJrApiOption _jrApiOption;

        public static void AddServiceDependencyJrApiSwagger(this IServiceCollection services, IConfiguration configuration, Func<IJrApiOption> options = null)
        {
            _jrApiOption = options() ?? new JrApiOption();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"V{_jrApiOption.MajorVersion}.{_jrApiOption.MinorVersion}", new OpenApiInfo
                {
                    Contact = new OpenApiContact
                    {
                        Email = _jrApiOption.Email,
                        Url = new Uri(_jrApiOption.Uri)
                    },
                    Title = _jrApiOption.Title,
                    Description = _jrApiOption.Description,
                });
            });

            services.AddScoped<IJrApiOption>(x => _jrApiOption);
            services.AddApiVersioning(o =>
            {
                o.UseApiBehavior = false;
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(_jrApiOption.MajorVersion, _jrApiOption.MinorVersion);

                o.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader(_jrApiOption.HeaderApiVersionReader),
                    new UrlSegmentApiVersionReader());
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = _jrApiOption.GroupNameFormat;
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(provider => new ConfigureSwaggerOptions(provider.GetService<IApiVersionDescriptionProvider>(), _jrApiOption));

            services.Configure<IConfigureOptions<SwaggerGenOptions>>(configuration);
        }
    }
}