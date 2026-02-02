using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}
