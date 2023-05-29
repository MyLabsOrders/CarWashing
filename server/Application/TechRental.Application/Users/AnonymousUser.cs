using TechRental.Application.Abstractions.Identity;
using TechRental.Application.Common.Exceptions;
using TechRental.Domain.Common.Exceptions;

namespace TechRental.Application.Users;

internal class AnonymousUser : ICurrentUser {
    public Guid Id => throw new UnauthorizedException("Attempt to access anonymous user id");

    public bool CanCreateUserWithRole(string roleName) {
        return roleName.Equals(TechRentalIdentityRoleNames.DefaultUserRoleName, StringComparison.Ordinal)
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