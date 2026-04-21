using CosmeticsShop.Infrastructure;
using CosmeticsShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly DataManager _data;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(DataManager data, UserManager<IdentityUser> userManager)
    {
        _data = data;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var cart = _data.Cart.GetOrCreateCart(userId);
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int productId, int quantity = 1)
    {
        var userId = _userManager.GetUserId(User)!;
        var product = _data.Products.GetProductById(productId);
        if (product == null) return NotFound();

        var cart = _data.Cart.GetOrCreateCart(userId);
        var existingItem = _data.Cart.GetCartItem(cart.Id, productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            _data.Cart.SaveCartItem(existingItem);
        }
        else
        {
            _data.Cart.SaveCartItem(new Domain.Entities.CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            });
        }

        TempData["Success"] = "Товар добавлен в корзину";
        return RedirectToAction("Show", "Products", new { id = productId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int itemId)
    {
        _data.Cart.DeleteCartItem(itemId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int itemId, int quantity)
    {
        if (quantity <= 0)
        {
            _data.Cart.DeleteCartItem(itemId);
        }
        else
        {
            var item = _data.Cart.GetOrCreateCart(_userManager.GetUserId(User)!);
            var cartItem = item.Items.FirstOrDefault(i => i.Id == itemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _data.Cart.SaveCartItem(cartItem);
            }
        }
        return RedirectToAction(nameof(Index));
    }
}
