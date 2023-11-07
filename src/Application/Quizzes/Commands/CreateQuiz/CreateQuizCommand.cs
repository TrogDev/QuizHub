namespace QuizHub.Application.Quizzes.Commands.CreateQuiz;

using MediatR;

public record CreateQuizCommand : IRequest<Guid>
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? MinuteLimit { get; set; }
}
