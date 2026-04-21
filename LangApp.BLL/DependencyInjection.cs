using LangApp.BLL.Auth.Interfaces;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using LangApp.BLL.Auth.Services;
using LangApp.Core.Interfaces;
using LangApp.BLL.Translations.Services;

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
        services.AddHttpClient<ILibreTranslateService, LibreTranslateService>();
        return services;
    }
}
