namespace QuizHub.Infrastructure.Auth.Common.Exceptions;

using Microsoft.AspNetCore.Identity;

public class InvalidRegisterException : Exception
{
    public InvalidRegisterException(IEnumerable<IdentityError> errors)
    {
        Errors = errors.ToDictionary(e => e.Code, e => e.Description);
    }

    public IDictionary<string, string> Errors { get; private set; }
}
