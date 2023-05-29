namespace CarWashing.Presentation.Contracts.Identity;

public record LoginResponse(Guid UserId, string Token);
