namespace QuizHub.Infrastructure.Auth.Common.Models;

public record AuthResponse
{
    public required long UserId { get; set; }
    public required AccessToken AccessToken { get; set; }
}
