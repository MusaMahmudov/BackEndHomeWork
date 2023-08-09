using HomeWorkPronia.Areas.Admin.ViewModels.UserViewModels;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HomeWorkPronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public  IActionResult Index()
        {
             var Users =  _context.Users;
            return View(Users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);
            var UserRole = await _context.UserRoles.FirstOrDefaultAsync(ur=>ur.UserId == User.Id);
            var role = await _context.Roles.FirstOrDefaultAsync(r=> r.Id == UserRole.RoleId);
           

            DetailUserViewModel detailUserViewModel = new DetailUserViewModel()
            {
                UserName = User.UserName,
                Email = User.Email,
                FullName = User.Fullname,
                Role = role.Name
            };
            return View(detailUserViewModel);
        }
        public async Task<IActionResult> ChangeRole(string id)
        {
            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(),"Id","Name");
            var RoleUser = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == id);
            if(RoleUser is null)
            {
                return NotFound();
            }
            UpdateUserViewModel updateUserViewModel = new UpdateUserViewModel()
            {
                RoleId = RoleUser.RoleId,
              
            };
            return View(updateUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(UpdateUserViewModel updateUserViewModel,string Id)
        {
            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "Id", "Name");
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == Id);

            if(userRole is  null)
            {
                return NotFound();
            }
    
            if (updateUserViewModel is not null)
            {
                _context.UserRoles.Add(new IdentityUserRole<string> { UserId = userRole.UserId, RoleId = updateUserViewModel.RoleId });
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            };
            

            return RedirectToAction(nameof(Index));

        }

    }
}
