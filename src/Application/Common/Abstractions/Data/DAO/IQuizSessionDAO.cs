namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Entities;

public interface IQuizSessionDAO : IBaseDAO<QuizSession, long>
{
    public Task<QuizSession?> Find(Guid quizId, long id);
    Task<IList<QuizSession>> FindAllUserClosed(long userId, Guid quizId);
}
