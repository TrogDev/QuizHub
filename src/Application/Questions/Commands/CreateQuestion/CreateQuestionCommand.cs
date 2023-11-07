namespace QuizHub.Application.Questions.Commands.CreateQuestion;

using MediatR;
using QuizHub.Domain.Enums;

public record CreateQuestionCommand : IRequest<long>
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required QuestionType Type { get; set; }
    public required Guid QuizId { get; set; }

    public required ICollection<CreateAnswerData> Answers { get; set; }
}

public record CreateAnswerData
{
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
}