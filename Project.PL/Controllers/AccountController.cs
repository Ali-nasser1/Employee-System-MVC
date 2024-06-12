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

		public AccountController(UserManager<ApplicationUser> _userManager)
        {
			userManager = _userManager;
		}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if(ModelState.IsValid)
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
                if(Result.Succeeded)
                {
                    return RedirectToAction("Login"); // nameof(Login)
				}
                else
                {
                    foreach(var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
		}
	}
}
