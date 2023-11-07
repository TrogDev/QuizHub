namespace QuizHub.Web.Api.Middlewares;

using Microsoft.AspNetCore.Mvc;

using QuizHub.Application.Common.Exceptions;

public class NotFoundMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EntityNotFoundException)
        {
            var response = new NotFoundResult();
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
