using FinManagerGf.Application.Auth;
using FinManagerGf.Application.Auth.Commands;
using FinManagerGf.Application.Auth.Queries;
using FinManagerGf.Shared.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinManagerGf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IdentityResult> Register(RegisterUserCommand command) =>
            await mediator.Send(command);


        [HttpPost("Login")]
        public async Task<AuthTokenDto> Login(LoginUserCommand command) => 
            await mediator.Send(command);


        [HttpGet("Profile"), Authorize]
        public async Task<UserDto> GetProfileByToken() =>
            await mediator.Send(new FetchUserProfileQuery(User.Claims.FirstOrDefault(x => x.Type == "id")!.Value));
        
    }
}
