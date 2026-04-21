using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly DataManager _data;
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileController(DataManager data, UserManager<IdentityUser> userManager)
    {
        _data = data;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User)!;

        var orders = _data.Orders.GetOrders()
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.DateCreated)
            .ToList();

        var reviews = _data.Reviews.GetReviews()
            .Where(r => r.AuthorId == userId)
            .OrderByDescending(r => r.DateCreated)
            .ToList();

        ViewBag.Orders  = orders;
        ViewBag.Reviews = reviews;
        return View();
    }
}
