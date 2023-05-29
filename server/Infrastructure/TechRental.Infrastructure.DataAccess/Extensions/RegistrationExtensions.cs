using TechRental.DataAccess.Abstractions;
using TechRental.Infrastructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TechRental.Infrastructure.DataAccess.Extensions;

public static class RegistrationExtensions {
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> action) {
        collection.AddDbContext<IDatabaseContext, DatabaseContext>(action);

        return collection;
    }

    public static Task UseDatabaseContext(this IServiceProvider provider) {
        DatabaseContext context = provider.GetRequiredService<DatabaseContext>();

        return context.Database.MigrateAsync();
    }
}