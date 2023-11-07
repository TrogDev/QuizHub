namespace QuizHub.Application.Common.Abstractions;

using System.Linq.Expressions;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> Criteria { get; }

    public bool IsSpecified(T e)
    {
        return Criteria.Compile().Invoke(e);
    }
}
