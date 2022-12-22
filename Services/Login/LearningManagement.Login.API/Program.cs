using AutoMapper;
using HealthChecks.UI.Client;
using LearningManagement.Application;
using LearningManagement.Common.Mappings;
using LearningManagement.Common.ResponseInterceptor;
using LearningManagement.Infrastructure;
using LearningManagement.Infrastructure.DbContexts;
using LearningManagement.Login.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container. LyCSN4SJkF2aGvvUgkYK
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });
builder.Services.SwaggerGenConfig();
builder.Services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("DefaultConnection"));
var origins = configuration.GetValue<string>("AllowedOrigins").Split(";");
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowedOriginCORSPolicy", policy => policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});
builder.Services.AddApplicationLayer(configuration);
builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowedOriginCORSPolicy");
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.SwaggerUIConfig(provider);
app.UseHttpsRedirection();
app.GlobalExceptionConfig();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    {
        var response = new ErrorResponseBody("Token is not available or unauthorize", StatusCodes.Status401Unauthorized);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});
using (var scope = app.Services.CreateScope())
{
    var appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    appDb.Database.Migrate();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});
app.MapControllers();

app.Run();
