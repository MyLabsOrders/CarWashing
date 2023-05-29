using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CarWashing.Application.Extensions;
using CarWashing.Infrastructure.DataAccess.Extensions;
using CarWashing.Infrastructure.Identity.Extensions;
using CarWashing.Presentation.Controllers;
using CarWashing.Presentation.Middlewares;
using CarWashing.Presentation.WebAPI.Configuration;

namespace CarWashing.Presentation.WebAPI.Extensions;

internal static class ServiceCollectionExtensions {
    internal static IServiceCollection ConfigureServices(
        this IServiceCollection collection,
        WebApiConfiguration webApiConfiguration,
        IConfigurationSection identityConfigurationSection,
        IConfiguration configuration) {
        collection
            .AddControllers()
            .AddApplicationPart(typeof(IControllerProjectMarker).Assembly)
            .AddControllersAsServices();

        string connectionString = webApiConfiguration.PostgresConfiguration
            .ToConnectionString(webApiConfiguration.DbNamesConfiguration.ApplicationDbName);

        collection
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c => {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                string xmlFilename = $"{typeof(IControllerProjectMarker).Assembly.GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            })
            .AddApplication(configuration)
            .AddDatabaseContext(o => o
                .UseNpgsql(connectionString));

        collection.AddIdentityConfiguration(
            identityConfigurationSection,
            o => o.UseNpgsql(
                webApiConfiguration.PostgresConfiguration.ToConnectionString(webApiConfiguration.DbNamesConfiguration
                    .IdentityDbName)));

        collection.AddTransient<ExceptionHandlingMiddleware>();

        return collection;
    }
}
