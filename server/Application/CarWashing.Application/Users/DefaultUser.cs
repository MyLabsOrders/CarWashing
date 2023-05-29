using CarWashing.Application.Abstractions.Identity;

namespace CarWashing.Application.Users;

internal class DefaultUser : ICurrentUser {
    public DefaultUser(Guid id) {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName) {
        return false;
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName) {
        return false;
    }

    public bool CanManageOrders() {
        return false;
    }

    public bool CanManageBalance() {
        return true;
    }
}