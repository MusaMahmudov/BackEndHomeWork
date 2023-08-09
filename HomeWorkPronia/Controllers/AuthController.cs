using HomeWorkPronia.Models.Identity;
using HomeWorkPronia.Services.Interfaces;
using HomeWorkPronia.ViewModels.LoginViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Text;
using System.IO;

namespace HomeWorkPronia.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IWebHostEnvironment webHostEnvironment,IMailService mailService)
        {
            _webHostEnvironment = webHostEnvironment;
            _mailService = mailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel,string? returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
            if(appUser is null)
            {
                appUser = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
                if (appUser is null)
                    ModelState.AddModelError("","Username/Email or password is incorrect"); 
                return View();

            }
         var signInResult =  await _signInManager.PasswordSignInAsync(appUser, loginViewModel.Password, loginViewModel.RememberMe, true);
            if(signInResult.IsLockedOut)
            {
                ModelState.AddModelError("","Ged gelersen");
                return View();
            }
            if(!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or password is incorrect");
                return View();
            }

            if (!appUser.LockoutEnabled)
            {
                appUser.LockoutEnabled = true;
                appUser.LockoutEnd = null;
                await _userManager.UpdateAsync(appUser);

            }

            if(returnUrl is not null)
            {
                return Redirect(returnUrl);
            }

           


            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));

        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var User = await _userManager.FindByEmailAsync(model.Email);
            if(User is null) 
            {
                 ModelState.AddModelError("Email","User not Found");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(User);

            var link = Url.Action("ResetPassword", "Auth", new { email = model.Email, token = token }, HttpContext.Request.Scheme);
            string body = await GeEmailTemplateAsync(link);
            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = model.Email,
                Subject = "Reset Password",
                Body = body
            };
            


          await  _mailService.SendEMailAsync(mailRequest);

            return RedirectToAction(nameof(Login));
        }
        private async Task<string> GeEmailTemplateAsync(string link)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "reset-password.html");
            using StreamReader streamReader = new StreamReader(path) ;
            
                string result = await streamReader.ReadToEndAsync();

            

            result.Replace("[link]",link);
            return result;
        }
        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token)) 
            {
                return  BadRequest();
            }

            var User = await _userManager.FindByEmailAsync(email);
            if(User is null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel,string email,string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var User = await _userManager.FindByEmailAsync(email);
            if (User is null)
            {
                return NotFound();
            }


         var IdentityResult =  await _userManager.ResetPasswordAsync(User, token, resetPasswordViewModel.NewPassword);
            if(!IdentityResult.Succeeded) 
            {
              foreach(var error in IdentityResult.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View();

            }

            return RedirectToAction(nameof(Login));
        }
    }
}
