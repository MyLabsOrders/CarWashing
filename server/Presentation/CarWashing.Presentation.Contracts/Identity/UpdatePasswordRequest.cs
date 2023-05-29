namespace CarWashing.Presentation.Contracts.Identity;

public record UpdatePasswordRequest(string CurrentPassword, string NewPassword);
