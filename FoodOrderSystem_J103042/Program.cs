using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodOrderSystem_J103042.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<FoodOrderSystem_J103042Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodOrderSystem_J103042Context") ?? throw new InvalidOperationException("Connection string 'FoodOrderSystem_J103042Context' not found.")));


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

// Ensure Database Schema is Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FoodOrderSystem_J103042Context>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
