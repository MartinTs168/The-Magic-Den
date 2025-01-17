using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static async Task CreateAdminRoleAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (await roleManager.RoleExistsAsync(AdminRole) == false)
        {
            var role = new IdentityRole<Guid>(AdminRole);
            await roleManager.CreateAsync(role);
            
        }
    }

    public static async Task CreateAdminAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.FindByEmailAsync(AdminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Adminov",
                UserName = AdminUserName,
                NormalizedUserName = AdminEmail,
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                Address = "London Street Admin 3",
                PhoneNumber = "0888123456",
            };

            var result = await userManager.CreateAsync(admin, "admin123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AdminRole);
            }
        }
    }
}