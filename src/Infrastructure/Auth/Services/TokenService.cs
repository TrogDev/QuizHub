namespace QuizHub.Infrastructure.Auth.Services;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using QuizHub.Infrastructure.Identity;
using QuizHub.Infrastructure.Auth.Common.Abstractions;
using QuizHub.Infrastructure.Auth.Common.Models;
using QuizHub.Infrastructure.Auth.Common.Options;

public class TokenService : ITokenService
{
    private readonly JwtAuthOptions options;

    public TokenService(JwtAuthOptions options)
    {
        this.options = options;
    }

    public AccessToken CreateAccessToken(ApplicationUser user, IEnumerable<string> roles)
    {
        List<Claim> claims = getClaims(user, roles);
        string token = createToken(claims);
        return new AccessToken() { Token = token, ExpiresIn = options.AccessLifeTimeSeconds };
    }

    private List<Claim> getClaims(ApplicationUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

        foreach (string role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }

    private string createToken(List<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(options.AccessLifeTimeSeconds)),
            signingCredentials: new SigningCredentials(
                options.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
