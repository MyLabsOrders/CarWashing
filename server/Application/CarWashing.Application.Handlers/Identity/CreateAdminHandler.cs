using MediatR;
using CarWashing.Application.Abstractions.Identity;
using static CarWashing.Application.Contracts.Identity.Commands.CreateAdmin;

namespace CarWashing.Application.Handlers.Identity;

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
            CarWashingIdentityRoleNames.AdminRoleName,
            cancellationToken);
    }
}