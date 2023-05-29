using Microsoft.AspNetCore.Identity;

namespace TechRental.Infrastructure.Identity.Entities;

internal class TechRentalIdentityRole : IdentityRole<Guid> {
    public TechRentalIdentityRole(string roleName)
        : base(roleName) { }

    protected TechRentalIdentityRole() { }
}