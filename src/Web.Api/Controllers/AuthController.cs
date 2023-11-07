namespace QuizHub.Web.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using MediatR;

using QuizHub.Infrastructure.Auth.Commands.Register;
using QuizHub.Infrastructure.Auth.Commands.Login;
using QuizHub.Infrastructure.Auth.Common.Exceptions;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender mediator;

    public AuthController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] LoginCommand command)
    {
        try
        {
            return Ok(await mediator.Send(command));
        }
        catch (InvalidLoginException)
        {
            return BadRequest("Invalid login or password");
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand command)
    {
        try
        {
            return Ok(await mediator.Send(command));
        }
        catch (InvalidRegisterException e)
        {
            foreach (var error in e.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
            return BadRequest(ModelState);
        }
    }
}
