namespace QuizHub.Infrastructure.Auth.Commands.Register;

using Microsoft.AspNetCore.Identity;

using MediatR;

using QuizHub.Infrastructure.Auth.Common.Models;
using QuizHub.Infrastructure.Auth.Common.Abstractions;
using QuizHub.Infrastructure.Auth.Common.Exceptions;
using QuizHub.Infrastructure.Identity;

public record RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ITokenService tokenService;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService
    )
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        ApplicationUser user = await createUser(request);

        IEnumerable<string> roles = await userManager.GetRolesAsync(user);

        AccessToken token = tokenService.CreateAccessToken(user, roles);

        return new AuthResponse() { UserId = user.Id, AccessToken = token };
    }

    private async Task<ApplicationUser> createUser(RegisterCommand request)
    {
        var user = new ApplicationUser() { UserName = request.UserName, Email = request.Email };
        IdentityResult result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return user;
        }
        else
        {
            throw new InvalidRegisterException(result.Errors);
        }
    }
}
