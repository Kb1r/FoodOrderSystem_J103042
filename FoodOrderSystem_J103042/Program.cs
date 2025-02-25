using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FoodOrderSystem_J103042.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection
builder.Services.AddDbContext<FoodOrderSystem_J103042Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodOrderSystem_J103042Context")));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<FoodOrderSystem_J103042Context>()
    .AddDefaultTokenProviders();

// Add Fake Email Sender (Prevents IEmailSender errors)
builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();

// Add Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


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
app.UseSession();


// Seed roles and users (Admin & Customer)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FoodOrderSystem_J103042Context>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        // Ensure the database is up-to-date
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();

        // Seed Admin & Customer users
        await SeedRolesAndUsers(roleManager, userManager, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding roles and users.");
    }
}

// Run the app
app.Run();

// ✅ Seed roles and default users (Admin & Customer)
async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ILogger logger)
{
    var roles = new List<string> { "Admin", "Customer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    await CreateUser(userManager, logger, "admin@example.com", "Admin@123", "Admin");
    await CreateUser(userManager, logger, "customer@example.com", "Customer@321!", "Customer");
}

// ✅ Helper method to create users
async Task CreateUser(UserManager<IdentityUser> userManager, ILogger logger, string email, string password, string role)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true // Prevents login issues due to email confirmation
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
            logger.LogInformation($"{role} user '{email}' created successfully.");
        }
        else
        {
            logger.LogError($"Failed to create {role} user '{email}':");
            foreach (var error in result.Errors)
            {
                logger.LogError($"- {error.Description}");
            }
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}

// ✅ Fake Email Sender (Prevents IEmailSender Errors)
public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"FAKE EMAIL SENT TO: {email} | Subject: {subject}");
        return Task.CompletedTask;
    }
}
