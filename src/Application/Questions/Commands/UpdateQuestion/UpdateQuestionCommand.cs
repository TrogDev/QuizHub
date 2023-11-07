namespace QuizHub.Application.Questions.Commands.UpdateQuestion;

using MediatR;
using QuizHub.Domain.Enums;

public record UpdateQuestionCommand : IRequest
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required QuestionType Type { get; set; }
    public required Guid QuizId { get; set; }

    public required ICollection<UpdateAnswerData> Answers { get; set; }
}

public record UpdateAnswerData
{
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
}
