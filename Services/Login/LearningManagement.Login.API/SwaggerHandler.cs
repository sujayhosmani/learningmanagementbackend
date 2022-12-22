using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LearningManagement.Login.API
{
    public static class SwaggerHandler
    {
        public static void SwaggerUIConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(c =>
            {
                //c.RouteTemplate = "swagger/{documentName}/swagger.json";
                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
                //{
                //   new OpenApiServer { Url = $"https://{httpReq.Host.Value}/tracking"}
                //});
            });

            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", $"{description.GroupName.ToUpperInvariant()}");
                    c.RoutePrefix = "swagger";
                }
                c.DocumentTitle = "Learning Management System Swagger UI";
            });
        }
   
        public static void SwaggerGenConfig(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                       Title = "LMS",
                       Version = $"v{description.ApiVersion}",
                       Description = "LMS des",
                    });
                }

                c.ResolveConflictingActions(x => x.First());

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT token authentication",
                    Description = "Enter JWT authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() },
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                //c.EnableAnnotations();
            });
        }
    }
}
