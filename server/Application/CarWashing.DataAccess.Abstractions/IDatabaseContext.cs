using CarWashing.Domain.Core.Orders;
using CarWashing.Domain.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarWashing.DataAccess.Abstractions;

public interface IDatabaseContext {
    DbSet<Order> Orders { get; }

    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}