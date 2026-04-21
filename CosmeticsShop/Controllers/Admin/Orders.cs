using CosmeticsShop.Domain.Enums;
using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminOrdersController : Controller
{
    private readonly DataManager _data;
    public AdminOrdersController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var orders = _data.Orders.GetOrders()
            .OrderByDescending(o => o.DateCreated)
            .ToList();
        return View("~/Views/Admin/Orders/Index.cshtml", orders);
    }

    public IActionResult Details(int id)
    {
        var order = _data.Orders.GetOrderById(id);
        if (order == null) return NotFound();
        return View("~/Views/Admin/Orders/Details.cshtml", order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeStatus(int id, OrderStatusEnum status)
    {
        var order = _data.Orders.GetOrderById(id);
        if (order == null) return NotFound();
        order.Status = status;
        _data.Orders.SaveOrder(order);
        return RedirectToAction(nameof(Details), new { id });
    }
}
