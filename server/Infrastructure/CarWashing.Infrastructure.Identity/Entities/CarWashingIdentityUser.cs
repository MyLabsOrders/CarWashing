using CarWashing.Application.Dto.Identity;
using Microsoft.AspNetCore.Identity;

namespace CarWashing.Infrastructure.Identity.Entities;

internal class CarWashingIdentityUser : IdentityUser<Guid> {
    public IdentityUserDto ToDto() {
        return new IdentityUserDto(Id, UserName ?? string.Empty);
    }
}