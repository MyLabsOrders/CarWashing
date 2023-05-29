namespace TechRental.Application.Abstractions.Identity;

public interface ICurrentUser {
    Guid Id { get; }

    bool CanCreateUserWithRole(string roleName);

    bool CanChangeUserRole(string currentRoleName, string newRoleName);

    bool CanManageOrders();

    bool CanManageBalance();
}