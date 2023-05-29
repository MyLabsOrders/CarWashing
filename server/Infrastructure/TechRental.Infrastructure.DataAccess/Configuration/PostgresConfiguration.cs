namespace TechRental.Infrastructure.DataAccess.Configuration;

public class PostgresConfiguration {
    public string Host { get; init; } = string.Empty;

    public int Port { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string SslMode { get; init; } = string.Empty;

    public string ToConnectionString(string dbName) {
        return $"Host={Host};Port={Port};Database={dbName};Username={Username};Password={Password};Ssl Mode={SslMode};";
    }
}