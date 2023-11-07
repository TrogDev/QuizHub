namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Models;

public interface IQuizDAO : IBaseDAO<Quiz, Guid>
{
    Task<Quiz?> Get(Guid id);
    Task<PaginatedList<Quiz>> GetFromUser(long userId, int perPage, int page);
}
