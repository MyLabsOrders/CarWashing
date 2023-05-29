using MediatR;
using TechRental.Application.Abstractions.Identity;
using static TechRental.Application.Contracts.Identity.Commands.CreateAdmin;

namespace TechRental.Application.Handlers.Identity;

internal class CreateAdminHandler : IRequestHandler<Command> {
    private readonly IAuthorizationService _authorizationService;

    public CreateAdminHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }


    public async Task Handle(Command request, CancellationToken cancellationToken) {
        await _authorizationService.CreateUserAsync(
            Guid.NewGuid(),
            request.Username,
            request.Password,
            TechRentalIdentityRoleNames.AdminRoleName,
            cancellationToken);
    }
}