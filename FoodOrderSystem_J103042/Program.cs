using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodOrderSystem_J103042.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection
builder.Services.AddDbContext<FoodOrderSystem_J103042Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodOrderSystem_J103042Context")));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<FoodOrderSystem_J103042Context>()
    .AddDefaultTokenProviders();

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Seed roles and users (Admin & Customer)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FoodOrderSystem_J103042Context>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    try
    {
        // Ensure the database is up-to-date
        await context.Database.MigrateAsync();

        // Seed Admin & Customer users
        await SeedRolesAndUsers(roleManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles and users.");
    }
}

// Run the app
app.Run();

// Seed roles and default users (Admin & Customer)
async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
    // Define roles
    var adminRole = "Admin";
    var customerRole = "Customer";

    // Create roles if they don't exist
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }
    if (!await roleManager.RoleExistsAsync(customerRole))
    {
        await roleManager.CreateAsync(new IdentityRole(customerRole));
    }

    // Create Admin User
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true // Prevents login issues due to email confirmation
        };

        var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdminResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
        else
        {
            Console.WriteLine("Failed to create Admin user:");
            foreach (var error in createAdminResult.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }

    // Create Customer User
    var customerEmail = "customer@example.com";
    var customerPassword = "Customer@321!";

    var customerUser = await userManager.FindByEmailAsync(customerEmail);
    if (customerUser == null)
    {
        customerUser = new IdentityUser
        {
            UserName = customerEmail,
            Email = customerEmail,
            EmailConfirmed = true
        };

        var createCustomerResult = await userManager.CreateAsync(customerUser, customerPassword);
        if (createCustomerResult.Succeeded)
        {
            await userManager.AddToRoleAsync(customerUser, customerRole);
        }
        else
        {
            Console.WriteLine("Failed to create Customer user:");
            foreach (var error in createCustomerResult.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(customerUser, customerRole))
        {
            await userManager.AddToRoleAsync(customerUser, customerRole);
        }
    }
}
