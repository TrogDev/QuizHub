namespace QuizHub.Application.Common.Abstractions.Identity;

public interface IIdentityService
{
    Task<bool> IsInRole(long userId, string role);
}
