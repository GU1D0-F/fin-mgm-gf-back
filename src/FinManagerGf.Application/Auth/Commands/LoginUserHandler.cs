using FinManagerGf.Domain.Entities;
using FinManagerGf.Shared.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinManagerGf.Application.Auth.Commands
{
    internal class LoginUserHandler(UserManager<User> userManager, SignInManager<User> signInManager) : IRequestHandler<LoginUserCommand, AuthTokenDto>
    {
        private const string issuer = "fin-dev-identity";
        private const string audience = "fin-manager-front";

        public async Task<AuthTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {         
            var user = await userManager.FindByEmailAsync(request.Email);
            List<string> errorsList = [];

            if (user == null)
            {
                errorsList.Add("User not found!");
                return new AuthTokenDto(null, false, errorsList, null);
            }

            if (!user.Admin)
            {
                errorsList.Add("User is not Admin");
                return new AuthTokenDto(null, false, errorsList, null);
            }
            var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                errorsList.Add("Login Failed!");
                return new AuthTokenDto(null, false, errorsList, null);
            }

            var token = GenerateToken(user);

            var (FirstName, LastName) = SplitFullName(user.FullName!);

            return new AuthTokenDto(token, true, null, new UserDto()
            {
                Id = user.Id,
                Username = user.UserName,
                First_Name = FirstName,
                Last_Name = LastName,
                Email = user.Email
            });
        }

        private static string GenerateToken(User user)
        {

            Claim[] claims =
            [
                new("sub", user.UserName!),
                new("id", user.Id),
                new("loginTimestamp", DateTime.UtcNow.ToString()),
            ];

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3A81F90D5A4E5E8A1C84E7D4B901D6BB67F4FD52415A84ECA7857D2B2E1A55F"));

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddHours(5),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
}
