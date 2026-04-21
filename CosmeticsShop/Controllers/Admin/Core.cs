using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminCoreController : Controller
{
    private readonly DataManager _data;

    public AdminCoreController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var allProducts = _data.Products.GetProducts().ToList();
        var allOrders   = _data.Orders.GetOrders().ToList();
        var today       = DateTime.UtcNow.Date;
        var monthStart  = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        ViewBag.TotalProducts  = allProducts.Count;
        ViewBag.TotalOrders    = allOrders.Count;
        ViewBag.TodayOrders    = allOrders.Count(o => o.DateCreated >= today);
        ViewBag.MonthRevenue   = allOrders
            .Where(o => o.DateCreated >= monthStart)
            .Sum(o => o.Total);
        ViewBag.RecentOrders   = allOrders
            .OrderByDescending(o => o.DateCreated)
            .Take(5)
            .ToList();
        ViewBag.NewReviews     = _data.Reviews.GetReviews()
            .OrderByDescending(r => r.DateCreated)
            .Take(5)
            .ToList();

        return View("~/Views/Admin/Index.cshtml");
    }
}
