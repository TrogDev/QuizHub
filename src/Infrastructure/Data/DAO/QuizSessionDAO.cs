namespace QuizHub.Infrastructure.Data.DAO;

using Microsoft.EntityFrameworkCore;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Application.Common.Specifications;
using QuizHub.Infrastructure.Data.Context;

public class QuizSessionDAO : BaseDAO<QuizSession, long>, IQuizSessionDAO
{
    public QuizSessionDAO(ApplicationDbContext context)
        : base(context) { }

    public async Task<QuizSession?> Find(Guid quizId, long id)
    {
        return await context.QuizSessions
            .Include(e => e.Quiz)
            .FirstOrDefaultAsync(e => e.Id == id && e.QuizId == quizId);
    }

    public async Task<IList<QuizSession>> FindAllUserClosed(long userId, Guid quizId)
    {
        var specification = new IsOpenQuizSessionSpecification();
        return await context.QuizSessions
            .Include(e => e.Quiz)
            .Include(e => e.Results)
            .Include($"{nameof(QuizSession.Results)}.{nameof(QuizSessionResult.Answers)}")
            .AsSplitQuery()
            .Where(specification.Criteria)
            .Where(e => e.UserId == userId && e.QuizId == quizId)
            .ToListAsync();
    }
}
