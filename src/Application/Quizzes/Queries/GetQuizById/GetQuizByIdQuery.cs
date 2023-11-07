namespace QuizHub.Application.Quizzes.Queries.GetQuizById;

using MediatR;

using QuizHub.Application.Quizzes.DTO;

public record GetQuizByIdQuery : IRequest<QuizDTO>
{
    public Guid Id { get; init; }
}
