namespace QuizHub.Infrastructure.Auth.Common.Abstractions;

using QuizHub.Infrastructure.Auth.Common.Models;
using QuizHub.Infrastructure.Identity;

public interface ITokenService
{
    AccessToken CreateAccessToken(ApplicationUser user, IEnumerable<string> roles);
}
