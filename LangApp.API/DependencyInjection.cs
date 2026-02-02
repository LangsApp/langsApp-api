using LangApp.DAL;
using LangApp.Core;
using LangApp.BLL;


namespace LangApp.API;

public static class DependencyInjection
{
    public static IServiceCollection Add_API_DI(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Add_DAL_DI().Add_BLL_DI().Add_Core_DI(configuration);

        return services;
    }
}
