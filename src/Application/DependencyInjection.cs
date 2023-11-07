namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FluentValidation;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Behaviours;
using QuizHub.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        services.AddTransient<IPermission<Quiz>, QuizPermission>();
        services.AddTransient<IPermission<Question>, QuestionPermission>();
        services.AddTransient<IPermission<QuizSession>, QuizSessionPermission>();

        return services;
    }
}
