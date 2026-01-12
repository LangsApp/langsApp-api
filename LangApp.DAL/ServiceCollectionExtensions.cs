using LangApp.DAL.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LangApp.Core.Options;


namespace LangApp.DAL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Add_DAL_DI(this IServiceCollection services)
    {
        services.AddDbContext<LangAppDBContext>((provider, options) =>
        {
            options.UseNpgsql(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>()
                .Value.DefaultConnection);
        });
        // Register repositories
        //services.AddScoped<IBaseWordRepository, BaseWordRepository>();
        //services.AddScoped<ICategoryRepository, CategoryRepository>();
        //services.AddScoped<ILanguagesRepository, LanguagesRepository>();
        //services.AddScoped<IProgressRepository, ProgressRepository>();
        //services.AddScoped<IStageRepository, StageRepository>();
        //services.AddScoped<ITranslateRepository, TranslateRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
