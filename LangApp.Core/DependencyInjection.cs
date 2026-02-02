using LangApp.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LangApp.Core;

public static class DependencyInjection
{
    public static IServiceCollection Add_Core_DI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
        return services;
    }
}
