using FinManagerGf.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinManagerGf.Application.Auth.Commands
{
    internal class RegisterUserHandler(UserManager<User> userManager) : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = new()
            {
                FullName = $"{request.FirstName} {request.LastName}",
                UserName = await GenerateUniqueUserName(request.FirstName, request.LastName),
                Email = request.Email,
                EmailConfirmed = false,
                TwoFactorEnabled = false,
            };

            return await userManager.CreateAsync(user, request.Password);
        }


        private async Task<string> GenerateUniqueUserName(string firstName, string lastName)
        {
            for(int i = 0; i <= 5; i++)
            {
                string baseUserName = $"{firstName}{lastName[0]}";
                string userName = baseUserName;


                var user = await userManager.FindByNameAsync(userName);

                if (user == null)
                    return userName;
            }

            //TODO: mandar uma mensagem q o front lida com a transcription
            throw new UserNameUnavailableException($"Não foi possível gerar um nome de usuário disponível para {firstName} {lastName} após várias tentativas.");
        }
    }

    public class UserNameUnavailableException(string message) : Exception(message)
    {
    }
}
