namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Entities;

public interface IAnswerDAO : IBaseDAO<Answer, long>
{
    Task<IList<Answer>> Find(long questionId, ICollection<long> ids);
}
