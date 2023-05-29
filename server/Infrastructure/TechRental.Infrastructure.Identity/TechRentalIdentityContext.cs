using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechRental.Infrastructure.Identity.Entities;

namespace TechRental.Infrastructure.Identity;

internal sealed class TechRentalIdentityContext : IdentityDbContext<TechRentalIdentityUser, TechRentalIdentityRole, Guid> {
    public TechRentalIdentityContext(DbContextOptions<TechRentalIdentityContext> options)
        : base(options) {
        Database.EnsureCreated();
    }
}