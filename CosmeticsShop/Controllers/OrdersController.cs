using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Enums;
using CosmeticsShop.Infrastructure;
using CosmeticsShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly DataManager _data;
    private readonly UserManager<IdentityUser> _userManager;

    public OrdersController(DataManager data, UserManager<IdentityUser> userManager)
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
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var order = _data.Orders.GetOrderById(id);
        if (order == null || order.UserId != userId) return NotFound();
        return View(order);
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var userId = _userManager.GetUserId(User)!;
        var cart = _data.Cart.GetCartByUserId(userId);
        if (cart == null || !cart.Items.Any())
            return RedirectToAction("Index", "Cart");

        var model = new CheckoutViewModel
        {
            Items = cart.Items.ToList(),
            Total = cart.Items.Sum(i => i.Product.Price * i.Quantity)
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(CheckoutViewModel model)
    {
        var userId = _userManager.GetUserId(User)!;
        var cart = _data.Cart.GetCartByUserId(userId);

        if (cart == null || !cart.Items.Any())
            return RedirectToAction("Index", "Cart");

        if (!ModelState.IsValid)
        {
            model.Items = cart.Items.ToList();
            model.Total = cart.Items.Sum(i => i.Product.Price * i.Quantity);
            return View(model);
        }

        var order = new Order
        {
            UserId          = userId,
            Status          = OrderStatusEnum.Pending,
            DeliveryAddress = model.DeliveryAddress,
            Phone           = model.Phone,
            Comment         = model.Comment,
            Total           = cart.Items.Sum(i => i.Product.Price * i.Quantity),
            Items           = cart.Items.Select(i => new OrderItem
            {
                ProductId      = i.ProductId,
                Quantity       = i.Quantity,
                PriceAtPurchase = i.Product.Price
            }).ToList()
        };

        _data.Orders.SaveOrder(order);
        _data.Cart.ClearCart(cart.Id);

        return RedirectToAction(nameof(Success), new { id = order.Id });
    }

    public IActionResult Success(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var order = _data.Orders.GetOrderById(id);
        if (order == null || order.UserId != userId) return NotFound();
        return View(order);
    }
}
