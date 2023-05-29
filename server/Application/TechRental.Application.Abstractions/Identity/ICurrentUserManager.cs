using System.Security.Claims;

namespace TechRental.Application.Abstractions.Identity;

public interface ICurrentUserManager {
    void Authenticate(ClaimsPrincipal principal);
}