namespace QuizHub.Infrastructure.Auth.Commands.Login;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using MediatR;

using QuizHub.Infrastructure.Auth.Common.Models;
using QuizHub.Infrastructure.Auth.Common.Abstractions;
using QuizHub.Infrastructure.Auth.Common.Exceptions;
using QuizHub.Infrastructure.Identity;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly ITokenService tokenService;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser user = await findUser(request.Login);
        await checkPassword(user, request.Password);
        IEnumerable<string> roles = await userManager.GetRolesAsync(user);
        
        AccessToken token = tokenService.CreateAccessToken(user, roles);

        return new AuthResponse() { UserId = user.Id, AccessToken = token };
    }

    private async Task<ApplicationUser> findUser(string login)
    {
        ApplicationUser? user =
            await userManager.FindByEmailAsync(login)
            ?? await userManager.FindByNameAsync(login);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        return user;
    }

    private async Task checkPassword(ApplicationUser user, string password)
    {
        SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
        {
            throw new InvalidLoginException();
        }
    }
}
