using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TechRental.Application.Abstractions.Identity;
using TechRental.Infrastructure.Identity.Entities;
using TechRental.Infrastructure.Identity.Services;
using TechRental.Infrastructure.Identity.Tools;

namespace TechRental.Infrastructure.Identity.Extensions;

public static class RegistrationExtensions {
    public static void AddIdentityConfiguration(
        this IServiceCollection collection,
        IConfigurationSection identityConfigurationSection,
        Action<DbContextOptionsBuilder> action) {
        var config = identityConfigurationSection.Get<IdentityConfiguration>();

        var options = identityConfigurationSection.GetSection("Options");
        collection.Configure<IdentityOptions>(options);

        collection.AddScoped<IAuthorizationService, AuthorizationService>();

        if (config is not null)
            collection.AddSingleton(config);

        collection.AddDbContext<TechRentalIdentityContext>(action);

        collection.AddIdentity<TechRentalIdentityUser, TechRentalIdentityRole>()
            .AddEntityFrameworkStores<TechRentalIdentityContext>()
            .AddDefaultTokenProviders();

        collection.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = config?.Audience,
                    ValidIssuer = config?.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config?.Secret ?? string.Empty)),
                };
                options.Events = new JwtBearerEvents {
                    OnTokenValidated = context => {
                        if (context.Principal is null)
                            return Task.CompletedTask;

                        var userManager = context.HttpContext.RequestServices
                            .GetRequiredService<ICurrentUserManager>();

                        userManager.Authenticate(context.Principal);
                        return Task.CompletedTask;
                    },
                };
            });
    }
}