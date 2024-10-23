using FinManagerGf.Shared.Dto;
using MediatR;

namespace FinManagerGf.Application.Auth
{
    public record LoginUserCommand : IRequest<AuthTokenDto>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
