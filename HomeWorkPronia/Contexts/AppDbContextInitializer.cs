using HomeWorkPronia.Models.Identity;
using HomeWorkPronia.Utils.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public AppDbContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task InitializerAsync()
        {
            await _context.Database.MigrateAsync(); 


        }

        public async Task UserSeedAsync()
        {
           foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString()});
            }
            AppUser adminUser = new AppUser()
            {
                UserName = "admin",
                Email = "admin@code.edu.az",
                Fullname = "AdminUser",
                IsActive = true,
            };
            await _userManager.CreateAsync(adminUser,"Musa2003!");

          await _userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());


        }
    }
}
