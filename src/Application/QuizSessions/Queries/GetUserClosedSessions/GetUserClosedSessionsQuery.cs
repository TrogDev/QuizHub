namespace QuizHub.Application.QuizSessions.Queries.GetUserClosedSessions;

using MediatR;
using QuizHub.Application.QuizSessions.DTO;

public record GetUserClosedSessionsQuery : IRequest<IList<QuizSessionDTO>>
{
    public required Guid QuizId { get; set; }
}
