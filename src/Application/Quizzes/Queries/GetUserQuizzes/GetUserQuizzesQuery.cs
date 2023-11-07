namespace QuizHub.Application.Quizzes.Queries.GetUserQuizzes;

using MediatR;
using QuizHub.Application.Common.Models;
using QuizHub.Application.Quizzes.DTO;

public record GetUserQuizzesQuery : IRequest<PaginatedList<QuizDTO>>
{
    public int Page { get; init; } = 1;
    public int PerPage { get; init; } = 20;
}
