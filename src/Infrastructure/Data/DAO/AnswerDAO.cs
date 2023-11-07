namespace QuizHub.Infrastructure.Data.DAO;

using Microsoft.EntityFrameworkCore;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;

public class AnswerDAO : BaseDAO<Answer, long>, IAnswerDAO
{
    public AnswerDAO(ApplicationDbContext context)
        : base(context) { }

    public async Task<IList<Answer>> Find(long questionId, ICollection<long> ids)
    {
        return await context.Answers
            .Where(e => e.QuestionId == questionId && ids.Contains(e.Id))
            .ToListAsync();
    }
}
