using Jr.Backend.Libs.API.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Jr.Backend.Libs.API.DependencyInjection
{
    public static class UseJrApi
    {
        public static void UseJrApiSwagger(this IApplicationBuilder app, IWebHostEnvironment env, Func<JrApiOption> options = null)
        {
            JrApiOption jrApiOption = new();
            if (options != null)
            {
                jrApiOption = options();
            }

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", jrApiOption.Title));
            }
        }

        public static void UseJrApiSwaggerSecurity(this IApplicationBuilder app, IWebHostEnvironment env, Func<JrApiOption> options = null)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            UseJrApiSwagger(app, env, options);
        }
    }
}