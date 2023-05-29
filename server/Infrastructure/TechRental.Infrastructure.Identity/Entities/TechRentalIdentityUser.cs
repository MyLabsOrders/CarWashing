using TechRental.Application.Dto.Identity;
using Microsoft.AspNetCore.Identity;

namespace TechRental.Infrastructure.Identity.Entities;

internal class TechRentalIdentityUser : IdentityUser<Guid> {
    public IdentityUserDto ToDto() {
        return new IdentityUserDto(Id, UserName ?? string.Empty);
    }
}