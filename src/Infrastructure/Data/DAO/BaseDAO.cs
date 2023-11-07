namespace QuizHub.Infrastructure.Data.DAO;

using Microsoft.EntityFrameworkCore;

using QuizHub.Domain.Common;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;

public abstract class BaseDAO<TEntity, TKey> : IBaseDAO<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    protected readonly ApplicationDbContext context;

    public BaseDAO(ApplicationDbContext context)
    {
        this.context = context;
    }

    public virtual async Task Create(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public virtual async Task Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}