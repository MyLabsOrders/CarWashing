using MediatR;

namespace CarWashing.Application.Contracts.Identity.Queries;

internal static class AuthorizeAdmin {
    public record Query(string Username) : IRequest;
}