namespace QuizHub.Application.QuizSessions.Commands.CreateQuizSession;

using MediatR;

public record CreateQuizSessionCommand : IRequest<long>
{
    public Guid QuizId { get; set; }
}
