using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CarWashing.Application.Contracts.Tools;

namespace CarWashing.Application.Handlers.Extensions;

public static class RegistrationExtensions {
    public static IServiceCollection AddHandlers(this IServiceCollection collection, IConfiguration configuration) {
        var paginationConfiguration = configuration.GetSection("Pagination").Get<PaginationConfiguration>()
            ?? throw new ArgumentException(nameof(configuration));

        collection.TryAddSingleton(paginationConfiguration);

        collection.AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(IAssemblyMarker)));

        return collection;
    }
}