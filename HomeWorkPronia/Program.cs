using AutoMapper;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Models.Identity;
using HomeWorkPronia.Services.Implementations;
using HomeWorkPronia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.AllowedForNewUsers = false;


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.ConfigureApplicationCookie(c =>
{
    c.LoginPath = "/Auth/Login";
});
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{Id?}"
    );
app.UseStaticFiles();

using (var scope  = app.Services.CreateScope())
{
  var initializer =  scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializerAsync();
    await initializer.UserSeedAsync();
}

app.Run();
