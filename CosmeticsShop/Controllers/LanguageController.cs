using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

public class LanguageController : Controller
{
    [HttpPost]
    public IActionResult Set(string lang, string? returnUrl)
    {
        if (lang == "ru" || lang == "kz")
        {
            Response.Cookies.Append("lang", lang, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true
            });
        }
        return Redirect(returnUrl ?? "/");
    }
}
