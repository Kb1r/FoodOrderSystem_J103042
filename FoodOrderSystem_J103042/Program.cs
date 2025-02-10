using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodOrderSystem_J103042.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Database Context
builder.Services.AddDbContext<FoodOrderSystem_J103042Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodOrderSystem_J103042Context")
    ?? throw new InvalidOperationException("Connection string 'FoodOrderSystem_J103042Context' not found.")));

// ✅ Add Identity Framework for Authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if email confirmation is needed
})
.AddEntityFrameworkStores<FoodOrderSystem_J103042Context>();

// ✅ Add Razor Pages with Authentication Support
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// ✅ Ensure Database Schema is Created & Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FoodOrderSystem_J103042Context>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.EnsureCreated();
    DbInitializer.Initialize(context); // Initialize DB with default data if necessary

    // Seed Admin User (Optional)
    var adminUser = new IdentityUser { UserName = "admin@site.com", Email = "admin@site.com", EmailConfirmed = true };
    if (await userManager.FindByEmailAsync(adminUser.Email) == null)
    {
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // ✅ Enable Authentication Middleware
app.UseAuthorization();

app.MapRazorPages();
app.Run();
