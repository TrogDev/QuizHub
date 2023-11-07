namespace QuizHub.Infrastructure.Data.DAO;

using Microsoft.EntityFrameworkCore;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;

public class QuizSessionResultDAO : BaseDAO<QuizSessionResult, long>, IQuizSessionResultDAO
{
    public QuizSessionResultDAO(ApplicationDbContext context)
        : base(context) { }

    public async Task<bool> isExists(long sessionId, long questionId)
    {
        return await context.QuizSessionResults.AnyAsync(
            e => e.QuizSessionId == sessionId && e.QuestionId == questionId
        );
    }
}
