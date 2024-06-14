using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.Models;
using Project.PL.ViewModels;

namespace Project.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
			userManager = _userManager;
			signInManager = _signInManager;
		}
        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    FName = model.FName,
                    LName = model.LName
                };
                var Result = await userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if(ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if(User != null)
                {
                  var Result = await userManager.CheckPasswordAsync(User, model.Password);
                    if(Result)
                    {
                      var LoginResult =  await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if(LoginResult.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    else
					 ModelState.AddModelError(string.Empty, "Password is incorrect!");
					
				}
                else
                 ModelState.AddModelError(string.Empty, "Email is not exist");
            }
            return View(model);
		}



		#endregion
	}
}
