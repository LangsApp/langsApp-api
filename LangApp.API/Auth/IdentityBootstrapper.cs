using LangApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LangApp.Core.Auth;

namespace LangApp.API.Auth;
//Server-site authorezetion <- check and try this
public static class IdentityBootstrapper
{
    public static async Task EnsureSuperAdminAsync(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var email = config["SuperAdmin:Email"];
        var password = config["SuperAdmin:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("SuperAdmin credentials are missing");
        }

        if (!await roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                FirstName = "Valerii",
                LastName = "Holub",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create SuperAdmin");
            }

            await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
        }
    }
}
