namespace QuizHub.Application.Questions.Commands.DeleteQuestion;

using MediatR;

public record DeleteQuestionCommand : IRequest
{
    public required Guid QuizId { get; set; }
    public required long Id { get; set; }
}
