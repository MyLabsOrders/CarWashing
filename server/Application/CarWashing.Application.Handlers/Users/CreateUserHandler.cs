using MediatR;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Core.Abstractions;
using CarWashing.Domain.Core.Users;
using CarWashing.Domain.Core.ValueObject;
using CarWashing.Infrastructure.Mapping.Users;
using static CarWashing.Application.Contracts.Users.Commands.CreateUser;

namespace CarWashing.Application.Handlers.Users;

internal class CreateUserHandler : IRequestHandler<Command, Response> {
    private readonly IDatabaseContext _context;

    public CreateUserHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {
        if (!Enum.TryParse(request.Gender, true, out Gender gender)) {
            throw new InvalidGenderException();
        }
        var user = new User(
            request.IdentityId,
            request.Firstname,
            request.Middlename,
            request.Lastname,
            new Image(request.UserImage),
            request.BirthDate,
            new PhoneNumber(request.PhoneNumber),
            gender);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(user.ToDto()!);
    }
}
