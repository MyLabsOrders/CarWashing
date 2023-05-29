using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechRental.Application.Abstractions.Identity;
using TechRental.Application.Handlers.Extensions;
using TechRental.Application.Users;

namespace TechRental.Application.Extensions;

public static class RegistrationExtensions {
    public static IServiceCollection AddApplication(this IServiceCollection collection, IConfiguration configuration) {
        collection.AddHandlers(configuration);
        collection.AddCurrentUser();

        return collection;
    }

    private static void AddCurrentUser(this IServiceCollection collection) {
        collection.AddScoped<CurrentUserProxy>();
        collection.AddScoped<ICurrentUser>(x => x.GetRequiredService<CurrentUserProxy>());
        collection.AddScoped<ICurrentUserManager>(x => x.GetRequiredService<CurrentUserProxy>());
    }
}