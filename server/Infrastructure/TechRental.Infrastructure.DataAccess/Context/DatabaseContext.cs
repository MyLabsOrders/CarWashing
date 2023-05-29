using TechRental.DataAccess.Abstractions;
using TechRental.Domain.Core.Orders;
using TechRental.Domain.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace TechRental.Infrastructure.DataAccess.Context;

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