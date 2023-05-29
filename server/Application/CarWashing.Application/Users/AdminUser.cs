using CarWashing.Application.Abstractions.Identity;

namespace CarWashing.Application.Users;

internal class AdminUser : ICurrentUser {
    public AdminUser(Guid id) {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName) {
        return true;
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName) {
        return true;
    }

    public bool CanManageOrders() {
        return true;
    }

    public bool CanManageBalance() {
        return false;
    }
}