namespace QuizHub.Infrastructure.Auth.Common.Models;

public record AccessToken
{
    public required string Token { get; set; }
    public required int ExpiresIn { get; set; }
}
