using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using CarWashing.Domain.Common.Exceptions;

namespace CarWashing.Application.Users;

internal class AnonymousUser : ICurrentUser {
    public Guid Id => throw new UnauthorizedException("Attempt to access anonymous user id");

    public bool CanCreateUserWithRole(string roleName) {
        return roleName.Equals(CarWashingIdentityRoleNames.DefaultUserRoleName, StringComparison.Ordinal)
                ? true
                : throw AccessDeniedException.AnonymousUserHasNotAccess();
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => throw AccessDeniedException.AnonymousUserHasNotAccess();


    public bool CanManageOrders()
        => throw AccessDeniedException.AnonymousUserHasNotAccess();

    public bool CanManageBalance()
        => throw AccessDeniedException.AnonymousUserHasNotAccess();
}