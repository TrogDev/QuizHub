namespace QuizHub.Infrastructure.Data.DAO;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

public class QuestionDAO : BaseDAO<Question, long>, IQuestionDAO
{
    public QuestionDAO(ApplicationDbContext context)
        : base(context) { }

    public async Task<Question?> GetWithQuiz(long id)
    {
        return await context.Questions
            .Include(e => e.Quiz)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Question?> Find(Guid quizId, long id)
    {
        return await context.Questions
            .Include(e => e.Quiz)
            .Include(e => e.Answers)
            .FirstOrDefaultAsync(e => e.QuizId == quizId && e.Id == id);
    }

    public async Task<IList<Question>> GetFromQuizWithAnswers(Guid quizId)
    {
        return await context.Questions
            .Include(e => e.Answers)
            .AsSplitQuery()
            .Where(e => e.QuizId == quizId)
            .ToListAsync();
    }
}
