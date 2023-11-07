namespace QuizHub.Web.Api.Services;

using System.Collections.Generic;
using System.Security.Claims;

using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Exceptions;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public long Id
    {
        get
        {
            string rawId = getCurrentUser().FindFirstValue(ClaimTypes.NameIdentifier);
            return long.Parse(rawId);
        }
    }

    public IList<string> Roles =>
        getCurrentUser().FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

    private ClaimsPrincipal getCurrentUser()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
        if (user is null)
            throw new UnauthorizedException();
        return user;
    }
}
