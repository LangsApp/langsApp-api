using LangApp.Core.Interfaces;
using LangApp.Core.Options;
using LangApp.DAL.DataContext;
using LangApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace LangApp.DAL;

public static class DependencyInjection
{
    public static IServiceCollection Add_DAL_DI(this IServiceCollection services)
    {
        services.AddDbContext<LangAppDBContext>((provider, options) =>
        {
            options.UseNpgsql(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>()
                .Value.DefaultConnection);
        });
        // Register repositories
        services.AddScoped<IBaseWord, BaseWordRepository>();
        //services.AddScoped<ICategoryRepository, CategoryRepository>();
        //services.AddScoped<ILanguagesRepository, LanguagesRepository>();
        //services.AddScoped<IProgressRepository, ProgressRepository>();
        //services.AddScoped<IStageRepository, StageRepository>();
        //services.AddScoped<ITranslateRepository, TranslateRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
