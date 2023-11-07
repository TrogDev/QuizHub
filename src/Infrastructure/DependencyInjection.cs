namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Auth.Common.Abstractions;
using QuizHub.Infrastructure.Auth.Common.Options;
using QuizHub.Infrastructure.Auth.Services;
using QuizHub.Infrastructure.Data;
using QuizHub.Infrastructure.Data.Context;
using QuizHub.Infrastructure.Data.DAO;
using QuizHub.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string connection = configuration.GetConnectionString("Default");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IQuizDAO, QuizDAO>();
        services.AddScoped<IQuestionDAO, QuestionDAO>();
        services.AddScoped<IAnswerDAO, AnswerDAO>();
        services.AddScoped<IQuizSessionDAO, QuizSessionDAO>();
        services.AddScoped<IQuizSessionResultDAO, QuizSessionResultDAO>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        addAuth(services, configuration);

        return services;
    }

    private static void addAuth(IServiceCollection services, IConfiguration configuration)
    {
        JwtAuthOptions options = configuration
            .GetRequiredSection("JwtAuthOptions")
            .Get<JwtAuthOptions>();

        services.AddSingleton(options);
        services.AddTransient<ITokenService, TokenService>();

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = getTokenValidationParameters(options);
            });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    private static TokenValidationParameters getTokenValidationParameters(JwtAuthOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = options.Issuer,
            ValidateAudience = true,
            ValidAudience = options.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = options.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    }
}
