using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
using CarWashing.Domain.Core.ValueObject;
using CarWashing.Infrastructure.Mapping.Users;
using static CarWashing.Application.Contracts.Users.Commands.UpdateUser;

namespace CarWashing.Application.Handlers.Users;

internal class UpdateUserHandler : IRequestHandler<Command, Response> {
    private readonly IDatabaseContext _context;

    public UpdateUserHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {

        if ((User?)await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken) is null)
        {
            throw EntityNotFoundException.For<User>(request.IdentityId);
        }

        if (request.Firstname is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).FirstName = request.Firstname;
        }

        if (request.Middlename is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).MiddleName = request.Middlename;
        }

        if (request.Lastname is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).LastName = request.Lastname;
        }

        if (request.PhoneNumber is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).PhoneNumber = new PhoneNumber(request.PhoneNumber);
        }

        if (request.UserImage is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).Image = request.UserImage;
        }

        if (request.BirthDate is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).BirthDate = (DateOnly)request.BirthDate;
        }

        if (request.Gender is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).Gender = Enum.Parse<Gender>(request.Gender, true);
        }

        if (request.IsActive is not null)
        {
            (await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).Status = (bool)request.IsActive;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Response((await _context.Users.FirstAsync(x => x.Id.Equals(request.IdentityId), cancellationToken)).ToDto()!);
    }
}
