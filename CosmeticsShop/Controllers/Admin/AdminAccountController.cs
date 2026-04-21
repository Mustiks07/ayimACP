using CosmeticsShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

public class AdminAccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signIn;
    private readonly UserManager<IdentityUser>   _userManager;

    public AdminAccountController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> userManager)
    {
        _signIn      = signIn;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true && User.IsInRole("Admin"))
            return RedirectToAction("Index", "AdminCore");
        return View("~/Views/Admin/Login.cshtml", new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View("~/Views/Admin/Login.cshtml", model);

        var result = await _signIn.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToAction("Index", "AdminCore");

            await _signIn.SignOutAsync();
            ModelState.AddModelError("", "Нет доступа. Требуются права администратора.");
        }
        else
        {
            ModelState.AddModelError("", "Неверный логин или пароль");
        }

        return View("~/Views/Admin/Login.cshtml", model);
    }
}
