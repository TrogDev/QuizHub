namespace QuizHub.Application.Common.Abstractions;

public interface IPermission<TEntity>
{
    bool IsCanEdit( IUser user, TEntity entity);
    bool IsCanDelete(IUser user, TEntity entity);
}