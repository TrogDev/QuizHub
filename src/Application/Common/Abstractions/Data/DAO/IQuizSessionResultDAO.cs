namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Entities;

public interface IQuizSessionResultDAO : IBaseDAO<QuizSessionResult, long>
{
    Task<bool> isExists(long sessionId, long questionId);
}
