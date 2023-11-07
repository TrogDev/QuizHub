namespace QuizHub.Application.QuizSessions.Commands.CloseQuizSession;

using MediatR;

public record CloseQuizSessionCommand : IRequest
{
    public Guid QuizId { get; set; }
    public long Id { get; set; }
}
