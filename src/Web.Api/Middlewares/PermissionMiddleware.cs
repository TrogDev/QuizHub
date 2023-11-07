namespace QuizHub.Web.Api.Middlewares;

using Microsoft.AspNetCore.Mvc;

using QuizHub.Application.Common.Exceptions;

public class PermissionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ForbiddenException)
        {
            var response = new ForbidResult();
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
