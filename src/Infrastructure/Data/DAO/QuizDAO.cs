namespace QuizHub.Infrastructure.Data.DAO;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Models;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;
using QuizHub.Infrastructure.Data.Services;

public class QuizDAO : BaseDAO<Quiz, Guid>, IQuizDAO
{
    public QuizDAO(ApplicationDbContext context)
        : base(context) { }

    public async Task<Quiz?> Get(Guid id)
    {
        return await context.Quizzes.FindAsync(id);
    }

    public async Task<PaginatedList<Quiz>> GetFromUser(long userId, int perPage, int page)
    {
        return await context.Quizzes
            .Where(e => e.AuthorId == userId)
            .GetPaginatedListAsync(perPage, page);
    }
}
