using FinManagerGf.Shared.Dto;
using MediatR;

namespace FinManagerGf.Application.Auth.Queries
{
    public record FetchUserProfileQuery(string UserId) : IRequest<UserDto>;
}
