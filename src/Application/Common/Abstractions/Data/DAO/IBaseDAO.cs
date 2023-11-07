namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Common;

public interface IBaseDAO<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task Create(TEntity entity);
    Task Delete(TEntity entity);
    Task Update(TEntity entity);
}
