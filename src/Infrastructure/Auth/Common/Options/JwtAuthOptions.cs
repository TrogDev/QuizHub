namespace QuizHub.Infrastructure.Auth.Common.Options;

using System.Text;

using Microsoft.IdentityModel.Tokens;

public class JwtAuthOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string PrivateKey { get; init; }
    public required int AccessLifeTimeSeconds { get; init; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
    }
}
