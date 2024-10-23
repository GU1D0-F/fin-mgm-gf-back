using FinManagerGf.Domain.Entities;
using FinManagerGf.Shared.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinManagerGf.Application.Auth.Queries
{
    internal class FetchUserProfileHandler(UserManager<User> userManager) : IRequestHandler<FetchUserProfileQuery, UserDto>
    {
        public async Task<UserDto> Handle(FetchUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId) ?? throw new NotFoundException("Nao encontrado");

            var (FirstName, LastName) = SplitFullName(user.FullName!);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                First_Name = FirstName,
                Last_Name = LastName,
                Email = user.Email
            };
        }

        private static (string FirstName, string LastName) SplitFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("O nome completo não pode estar vazio ou nulo.");

            var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (nameParts.Length == 1)
                return (nameParts[0], string.Empty);


            return (nameParts[0], nameParts[^1]);
        }
    }

    public class NotFoundException(string message) : Exception(message)
    {
    }
}
