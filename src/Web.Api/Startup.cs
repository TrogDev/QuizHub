namespace QuizHub.Web.Api;

using System.Reflection;

using QuizHub.Application.Common.Abstractions;
using QuizHub.Infrastructure.Data.Context;
using QuizHub.Web.Api.Middlewares;
using QuizHub.Web.Api.Services;

internal class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMvc()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
        services.AddInfrastructureServices(Configuration);
        services.AddApplicationServices();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IUser, CurrentUser>();
        services.AddTransient<ValidationMiddleware>();
        services.AddTransient<PermissionMiddleware>();
        services.AddTransient<NotFoundMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<ValidationMiddleware>();
        app.UseMiddleware<PermissionMiddleware>();
        app.UseMiddleware<NotFoundMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        initialiseDatabase(app);
    }

    private void initialiseDatabase(IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        var initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.Initialise();
    }
}
