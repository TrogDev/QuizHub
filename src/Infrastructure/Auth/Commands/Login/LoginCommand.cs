namespace QuizHub.Infrastructure.Auth.Commands.Login;

using MediatR;

using QuizHub.Infrastructure.Auth.Common.Models;

public record LoginCommand : IRequest<AuthResponse>
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}
