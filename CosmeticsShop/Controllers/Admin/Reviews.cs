using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminReviewsController : Controller
{
    private readonly DataManager _data;
    public AdminReviewsController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var reviews = _data.Reviews.GetReviews()
            .OrderByDescending(r => r.DateCreated)
            .ToList();
        return View("~/Views/Admin/Reviews/Index.cshtml", reviews);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _data.Reviews.DeleteReview(id);
        return RedirectToAction(nameof(Index));
    }
}
