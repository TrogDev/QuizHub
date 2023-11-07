namespace QuizHub.Application.Common.Abstractions;

public interface IUser
{
    long Id { get; }
    IList<string> Roles { get; }
}
