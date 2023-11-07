namespace QuizHub.Infrastructure.Auth.Commands.Register;

using MediatR;

using QuizHub.Infrastructure.Auth.Common.Models;

public record RegisterCommand : IRequest<AuthResponse>
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
