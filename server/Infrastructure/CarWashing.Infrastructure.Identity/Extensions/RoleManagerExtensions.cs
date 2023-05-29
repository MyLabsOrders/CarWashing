using Microsoft.AspNetCore.Identity;
using CarWashing.Infrastructure.Identity.Entities;

namespace CarWashing.Infrastructure.Identity.Extensions;

internal static class RoleManagerExtensions {
    public static async Task CreateIfNotExistsAsync(
        this RoleManager<CarWashingIdentityRole> roleManager,
        string roleName,
        CancellationToken cancellationToken = default) {

        if (await roleManager.RoleExistsAsync(roleName) is false)
        {
            await roleManager.CreateAsync(new CarWashingIdentityRole(roleName));
        }
    }
}
