namespace QuizHub.Application.Services;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;

public class QuizPermission : IPermission<Quiz>
{
    public bool IsCanDelete(IUser user, Quiz entity)
    {
        return entity.AuthorId == user.Id;
    }

    public bool IsCanEdit(IUser user, Quiz entity)
    {
        return entity.AuthorId == user.Id;
    }
}
