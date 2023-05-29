using TechRental.Application.Dto.Identity;

namespace TechRental.Application.Abstractions.Identity;

public interface IAuthorizationService {
    Task<string> GetUserTokenAsync(string username, CancellationToken cancellationToken);

    Task AuthorizeAdminAsync(string username, CancellationToken cancellationToken = default);

    Task CreateRoleIfNotExistsAsync(string roleName, CancellationToken cancellationToken = default);

    Task<IdentityUserDto> CreateUserAsync(
        Guid userId,
        string username,
        string password,
        string roleName,
        CancellationToken cancellationToken = default);

    Task<IdentityUserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<IdentityUserDto>> GetUsersByIdsAsync(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default);

    Task<IdentityUserDto> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default);

    Task UpdateUserNameAsync(Guid userId, string newUserName, CancellationToken cancellationToken = default);

    Task<IdentityUserDto> UpdateUserPasswordAsync(
        Guid userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default);

    Task UpdateUserRoleAsync(Guid userId, string newRoleName, CancellationToken cancellationToken = default);

    Task<string> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> CheckUserPasswordAsync(Guid userId, string password, CancellationToken cancellationToken = default);
}