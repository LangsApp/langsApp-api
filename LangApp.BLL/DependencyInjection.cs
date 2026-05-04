using LangApp.BLL.Auth.Interfaces;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using LangApp.BLL.Auth.Services;
using LangApp.BLL.Translations.Services;
using LangApp.Core.Interfaces.Services;

namespace LangApp.BLL;

public static class DependencyInjection
{
    public static IServiceCollection Add_BLL_DI(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.NotificationPublisher = new TaskWhenAllPublisher();
        });
        services.AddScoped<IAuthService, AuthService>();
        services.AddHttpClient<ITranslateService, TranslateService>();
        return services;
    }
}
