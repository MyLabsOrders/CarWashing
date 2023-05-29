using Microsoft.AspNetCore.Identity;
using TechRental.Infrastructure.Identity.Entities;

namespace TechRental.Infrastructure.Identity.Extensions;

internal static class RoleManagerExtensions {
    public static async Task CreateIfNotExistsAsync(
        this RoleManager<TechRentalIdentityRole> roleManager,
        string roleName,
        CancellationToken cancellationToken = default) {

        if (await roleManager.RoleExistsAsync(roleName) is false)
        {
            await roleManager.CreateAsync(new TechRentalIdentityRole(roleName));
        }
    }
}
