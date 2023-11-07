namespace QuizHub.Application.Services;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;

public class QuizSessionPermission : IPermission<QuizSession>
{
    public bool IsCanDelete(IUser user, QuizSession entity)
    {
        return entity.UserId == user.Id;
    }

    public bool IsCanEdit(IUser user, QuizSession entity)
    {
        return entity.UserId == user.Id;
    }
}
