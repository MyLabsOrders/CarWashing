using Microsoft.AspNetCore.Identity;

namespace CarWashing.Infrastructure.Identity.Entities;

internal class CarWashingIdentityRole : IdentityRole<Guid> {
    public CarWashingIdentityRole(string roleName)
        : base(roleName) { }

    protected CarWashingIdentityRole() { }
}