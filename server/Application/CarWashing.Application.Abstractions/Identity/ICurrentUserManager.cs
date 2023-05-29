using System.Security.Claims;

namespace CarWashing.Application.Abstractions.Identity;

public interface ICurrentUserManager {
    void Authenticate(ClaimsPrincipal principal);
}