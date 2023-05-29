using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarWashing.Infrastructure.Identity.Entities;

namespace CarWashing.Infrastructure.Identity;

internal sealed class CarWashingIdentityContext : IdentityDbContext<CarWashingIdentityUser, CarWashingIdentityRole, Guid> {
    public CarWashingIdentityContext(DbContextOptions<CarWashingIdentityContext> options)
        : base(options) {
        Database.EnsureCreated();
    }
}