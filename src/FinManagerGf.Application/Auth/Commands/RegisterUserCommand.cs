using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinManagerGf.Application.Auth.Commands
{
    public record RegisterUserCommand : IRequest<IdentityResult>
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
