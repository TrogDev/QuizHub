namespace QuizHub.Application.Quizzes.Commands.UpdateQuizz;

using MediatR;

public record UpdateQuizCommand : IRequest
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? MinuteLimit { get; set; }
}
