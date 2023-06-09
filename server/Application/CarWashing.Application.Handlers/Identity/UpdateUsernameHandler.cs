using MediatR;
using CarWashing.Application.Dto.Identity;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Domain.Common.Exceptions;
using static CarWashing.Application.Contracts.Identity.Commands.UpdateUsername;

namespace CarWashing.Application.Handlers.Identity;

internal class UpdateUsernameHandler : IRequestHandler<Command, Response> {
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public UpdateUsernameHandler(ICurrentUser currentUser, IAuthorizationService authorizationService) {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {
        IdentityUserDto user = await _authorizationService.GetUserByIdAsync(_currentUser.Id, cancellationToken);

        if (user.Username == request.Username)
        {
            throw UserInputException.UpdateUsernameFailedException("the old username is the same as the new one");
        }

        await _authorizationService.UpdateUserNameAsync(_currentUser.Id, request.Username, cancellationToken);

        return new Response(await _authorizationService.GetUserTokenAsync(request.Username, cancellationToken));
    }
}