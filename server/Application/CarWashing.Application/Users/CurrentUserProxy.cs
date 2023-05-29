using System.Security.Claims;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using CarWashing.Domain.Common.Exceptions;

namespace CarWashing.Application.Users;

public class CurrentUserProxy : ICurrentUser, ICurrentUserManager {
    private ICurrentUser _user = new AnonymousUser();

    public Guid Id => _user.Id;

    public bool CanCreateUserWithRole(string roleName) {
        return _user.CanCreateUserWithRole(roleName);
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName) {
        return _user.CanChangeUserRole(currentRoleName, newRoleName);
    }

    public bool CanManageOrders() {
        return _user.CanManageOrders();
    }

    public bool CanManageBalance() {
        return _user.CanManageBalance();
    }

    public void Authenticate(ClaimsPrincipal principal) {
        List<string> roles = principal.Claims
            .Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Value)
            .ToList();

        string nameIdentifier = principal.Claims
            .Single(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))
            .Value;

        if (!Guid.TryParse(nameIdentifier, out Guid id)) {
            throw new UnauthorizedException("Failed parse name identifier to guid");
        }

        if (roles.Contains(CarWashingIdentityRoleNames.AdminRoleName)) {
            _user = new AdminUser(id);
        }
        else {
            _user = roles.Contains(CarWashingIdentityRoleNames.DefaultUserRoleName) ? new DefaultUser(id) : new AnonymousUser();
        }
    }
}