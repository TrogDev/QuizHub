namespace QuizHub.Application.QuizSessionResults.Commands.SendAnswer;

using MediatR;

using QuizHub.Application.QuizSessionResults.DTO;

public class SendAnswerCommand : IRequest<QuizSessionResultDTO>
{
    public required Guid QuizId { get; set; }
    public required long SessionId { get; set; }
    public required long QuestionId { get; set; }
    public required ICollection<long> AnswerIds { get; set; }
}
