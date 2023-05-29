using TechRental.Application.Abstractions.Identity;

namespace TechRental.Application.Users;

internal class AdminUser : ICurrentUser {
    public AdminUser(Guid id) {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName)
        => true;

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => true;

    public bool CanManageOrders()
        => true;

    public bool CanManageBalance()
        => false;
}