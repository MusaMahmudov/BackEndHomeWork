using AutoMapper;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Models.Identity;
using HomeWorkPronia.Services.Interfaces;
using HomeWorkPronia.Utils.Enums;
using HomeWorkPronia.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace HomeWorkPronia.Controllers;

public class AccountController : Controller
{
    private readonly IMapper _mapper;
  
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Register()
    {

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateAccountViewModel createAccountViewModel)
    {
      
        if(!ModelState.IsValid)
        {
            return View();
        }

        AppUser newUser = _mapper.Map<AppUser>(createAccountViewModel);
        newUser.IsActive = true;


        IdentityResult identityResult = await _userManager.CreateAsync(newUser,createAccountViewModel.Password);

        if(!identityResult.Succeeded)
        {
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("",error.Description);

            }
            return View();
        }

        await _userManager.AddToRoleAsync(newUser,Roles.Member.ToString());

        return RedirectToAction("Login","Auth");

    }
    //public async Task<IActionResult> CreateRolesAsync()
    //{
    //    foreach(var role in Enum.GetValues(typeof(Roles)))
    //    {
    //        await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });

    //    }

        

    //    return Content("ok");

    //}

}
