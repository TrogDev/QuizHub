namespace QuizHub.Web.Api.Middlewares;

using Microsoft.AspNetCore.Mvc;

using QuizHub.Application.Common.Exceptions;

public class ValidationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            var response = new BadRequestObjectResult(e.Errors);
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
