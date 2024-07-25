using EcommerseProject.Data;
using EcommerseProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using EcommerseProject.Identity;
using Microsoft.Extensions.Hosting;
using EcommerseProject.Areas.Identity.Pages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors.Infrastructure;
using EcommerseProject.Services;
using EcommerseProject;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CarDealershipContextConnection")
    ?? throw new InvalidOperationException("Connection string 'CarDealershipContextConnection' not found.");

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CarDealershipContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarDealershipContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedRoles.Initialize(services).Wait();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); 

app.MapRazorPages();

app.Run();
