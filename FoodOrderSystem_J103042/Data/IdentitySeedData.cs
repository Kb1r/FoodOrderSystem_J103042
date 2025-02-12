using FoodOrderSystem_J103042.Data;
using Microsoft.AspNetCore.Identity;

public static class IdentitySeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<FoodOrderSystem_J103042Context>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Admin@123");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var memberUser = await userManager.FindByEmailAsync("member@example.com");
        if (memberUser == null)
        {
            memberUser = new IdentityUser
            {
                UserName = "member@example.com",
                Email = "member@example.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(memberUser, "Member@123");
            await userManager.AddToRoleAsync(memberUser, "Member");
        }
    }
}