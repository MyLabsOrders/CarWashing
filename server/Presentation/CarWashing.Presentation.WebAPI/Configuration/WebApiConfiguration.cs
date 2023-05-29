using CarWashing.Infrastructure.DataAccess.Configuration;

namespace CarWashing.Presentation.WebAPI.Configuration;

internal class WebApiConfiguration {
    public WebApiConfiguration(IConfiguration configuration) {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var postgresConfiguration = configuration
            .GetSection(nameof(PostgresConfiguration))
            .Get<PostgresConfiguration>();

        PostgresConfiguration = postgresConfiguration ??
                                throw new ArgumentException(nameof(PostgresConfiguration));

        var dbNameConfiguration = configuration
            .GetSection(nameof(DbNamesConfiguration))
            .Get<DbNamesConfiguration>();

        DbNamesConfiguration = dbNameConfiguration ??
                               throw new ArgumentException(nameof(DbNamesConfiguration));
    }

    public PostgresConfiguration PostgresConfiguration { get; set; }
    public DbNamesConfiguration DbNamesConfiguration { get; set; }
}