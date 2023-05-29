using Microsoft.AspNetCore.Identity;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
using CarWashing.Infrastructure.Identity.Entities;

namespace CarWashing.Infrastructure.Identity.Extensions;

internal static class UserManagerExtensions {
    public static async Task<CarWashingIdentityUser> GetByIdAsync(
        this UserManager<CarWashingIdentityUser> userManager,
        Guid userId,
        CancellationToken cancellationToken = default) {
        return await userManager.FindByIdAsync(userId.ToString()) ?? throw EntityNotFoundException.For<User>(userId);
    }

    public static async Task<CarWashingIdentityUser> GetByNameAsync(
        this UserManager<CarWashingIdentityUser> userManager,
        string username,
        CancellationToken cancellationToken = default) {
        return await userManager.FindByNameAsync(username) ?? throw EntityNotFoundException.For<User>(username);
    }
}
