using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarWashing.Infrastructure.Identity.Entities;
using CarWashing.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CarWashing.Application.Dto.Identity;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Infrastructure.Identity.Extensions;
using CarWashing.Infrastructure.Identity.Tools;

namespace CarWashing.Infrastructure.Identity.Services;

internal class AuthorizationService : IAuthorizationService {
    private readonly UserManager<CarWashingIdentityUser> _userManager;
    private readonly RoleManager<CarWashingIdentityRole> _roleManager;
    private readonly IdentityConfiguration _configuration;

    public AuthorizationService(
        UserManager<CarWashingIdentityUser> userManager,
        RoleManager<CarWashingIdentityRole> roleManager,
        IdentityConfiguration configuration) {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task AuthorizeAdminAsync(string username, CancellationToken cancellationToken = default) {
        CarWashingIdentityUser? user = await _userManager.FindByNameAsync(username);

        if (user is not null && await _userManager.IsInRoleAsync(user, CarWashingIdentityRoleNames.AdminRoleName))
            return;

        throw new UnauthorizedException("User is not admin");
    }

    public async Task CreateRoleIfNotExistsAsync(string roleName, CancellationToken cancellationToken = default) {
        await _roleManager.CreateIfNotExistsAsync(roleName, cancellationToken);
    }

    public async Task<IdentityUserDto> CreateUserAsync(
        Guid userId,
        string username,
        string password,
        string roleName,
        CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = new() {
            Id = userId,
            UserName = username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        result.EnsureSucceeded();

        await _userManager.AddToRoleAsync(user, roleName.ToLower());

        return user.ToDto();
    }

    public async Task<IdentityUserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);

        return user.ToDto();
    }

    public async Task<IEnumerable<IdentityUserDto>> GetUsersByIdsAsync(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default) {
        List<CarWashingIdentityUser> users = await _userManager.Users
            .Where(x => userIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        return users.Select(x => x.ToDto());
    }

    public async Task<IdentityUserDto> GetUserByNameAsync(
        string username,
        CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByNameAsync(username, cancellationToken);

        return user.ToDto();
    }

    public async Task UpdateUserNameAsync(
        Guid userId,
        string newUserName,
        CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);

        user.UserName = newUserName;

        IdentityResult result = await _userManager.UpdateAsync(user);

        result.EnsureSucceeded();
    }

    public async Task<IdentityUserDto> UpdateUserPasswordAsync(
        Guid userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default) {
        if (currentPassword.Equals(newPassword, StringComparison.Ordinal))
        {
            throw UserInputException.IdentityOperationNotSucceededException("New password cannot be the same as old password");
        }

        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);

        IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        result.EnsureSucceeded();

        return user.ToDto();
    }

    public async Task UpdateUserRoleAsync(
        Guid userId,
        string newRoleName,
        CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, newRoleName);
    }

    public async Task<string> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        return roles.Single();
    }

    public async Task<bool> CheckUserPasswordAsync(
        Guid userId,
        string password,
        CancellationToken cancellationToken = default) {
        CarWashingIdentityUser user = await _userManager.GetByIdAsync(userId, cancellationToken);

        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<string> GetUserTokenAsync(string username, CancellationToken cancellationToken) {
        CarWashingIdentityUser user = await _userManager.GetByNameAsync(username, cancellationToken);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        IEnumerable<Claim> claims = roles
            .Select(userRole => new Claim(ClaimTypes.Role, userRole))
            .Append(new Claim(ClaimTypes.Name, username ?? string.Empty))
            .Append(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()))
            .Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

        JwtSecurityToken token = new(
            _configuration.Issuer,
            _configuration.Audience,
            expires: DateTime.UtcNow.AddHours(_configuration.ExpiresHours),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
