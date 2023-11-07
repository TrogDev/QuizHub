namespace QuizHub.Application.Common.Specifications;

using System.Linq.Expressions;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;

public class IsOpenQuizSessionSpecification : Specification<QuizSession>
{
    public override Expression<Func<QuizSession, bool>> Criteria =>
        (e) =>
            e.IsClosed
            || (
                e.Quiz.MinuteLimit != null
                && DateTime.UtcNow > e.CreatedAt.AddMinutes((int)e.Quiz.MinuteLimit)
            );
}
