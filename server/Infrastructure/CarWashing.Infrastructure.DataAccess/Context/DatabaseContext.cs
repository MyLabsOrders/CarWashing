using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Domain.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarWashing.Infrastructure.DataAccess.Context;

public class DatabaseContext : DbContext, IDatabaseContext {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) {
        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; protected init; } = null!;
    public DbSet<User> Users { get; protected init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}