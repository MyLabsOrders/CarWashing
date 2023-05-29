using Microsoft.AspNetCore.Identity;
using TechRental.Domain.Common.Exceptions;
using TechRental.Domain.Core.Users;
using TechRental.Infrastructure.Identity.Entities;

namespace TechRental.Infrastructure.Identity.Extensions;

internal static class UserManagerExtensions {
    public static async Task<TechRentalIdentityUser> GetByIdAsync(
        this UserManager<TechRentalIdentityUser> userManager,
        Guid userId,
        CancellationToken cancellationToken = default) {
        return await userManager.FindByIdAsync(userId.ToString()) ?? throw EntityNotFoundException.For<User>(userId);
    }

    public static async Task<TechRentalIdentityUser> GetByNameAsync(
        this UserManager<TechRentalIdentityUser> userManager,
        string username,
        CancellationToken cancellationToken = default) {
        return await userManager.FindByNameAsync(username) ?? throw EntityNotFoundException.For<User>(username);
    }
}
