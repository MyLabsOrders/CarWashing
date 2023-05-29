using System.Security.Claims;
using TechRental.Application.Abstractions.Identity;
using TechRental.Application.Common.Exceptions;
using TechRental.Domain.Common.Exceptions;

namespace TechRental.Application.Users;

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
        var roles = principal.Claims
            .Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Value)
            .ToList();

        var nameIdentifier = principal.Claims
            .Single(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))
            .Value;

        if (!Guid.TryParse(nameIdentifier, out Guid id))
            throw new UnauthorizedException("Failed parse name identifier to guid");

        if (roles.Contains(TechRentalIdentityRoleNames.AdminRoleName))
            _user = new AdminUser(id);
        else if (roles.Contains(TechRentalIdentityRoleNames.DefaultUserRoleName))
            _user = new DefaultUser(id);
        else _user = new AnonymousUser();
    }
}