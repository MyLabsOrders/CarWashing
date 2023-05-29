using CarWashing.Application.Abstractions.Identity;

namespace CarWashing.Application.Users;

internal class DefaultUser : ICurrentUser {
    public DefaultUser(Guid id) {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName)
        => false;

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => false;

    public bool CanManageOrders()
        => false;

    public bool CanManageBalance()
        => true;
}