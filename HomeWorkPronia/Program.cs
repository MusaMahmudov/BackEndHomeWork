using AutoMapper;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Services.Implementations;
using HomeWorkPronia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{Id?}"
    );
app.UseStaticFiles();



app.Run();
