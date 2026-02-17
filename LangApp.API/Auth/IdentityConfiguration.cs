using LangApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using LangApp.DAL.DataContext;


namespace LangApp.API.Auth;

public static class IdentityConfiguration
{
    public static IServiceCollection Add_Identity_Configuration(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<LangAppDBContext>()
        .AddDefaultTokenProviders();
        return services;
    }
}
