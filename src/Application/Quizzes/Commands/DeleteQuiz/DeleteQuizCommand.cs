namespace QuizHub.Application.Quizzes.Commands.DeleteQuiz;

using MediatR;

public record DeleteQuizCommand : IRequest
{
    public required Guid Id { get; set; }
}
