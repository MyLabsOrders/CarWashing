namespace CarWashing.Presentation.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string MiddleName,
    string LastName,
    string PhoneNumber,
    string? UserImage,
    DateOnly BirthDate,
    string Gender);
