namespace QuizHub.Application.Services;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;

public class QuestionPermission : IPermission<Question>
{
    public bool IsCanDelete(IUser user, Question entity)
    {
        return entity.Quiz.AuthorId == user.Id;
    }

    public bool IsCanEdit(IUser user, Question entity)
    {
        return entity.Quiz.AuthorId == user.Id;
    }
}
