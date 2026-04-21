using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFCartRepository : ICartRepository
{
    private readonly AppDbContext _db;
    public EFCartRepository(AppDbContext db) => _db = db;

    public Cart? GetCartByUserId(string userId) =>
        _db.Carts
            .Include(c => c.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Brand)
            .FirstOrDefault(c => c.UserId == userId);

    public Cart GetOrCreateCart(string userId)
    {
        var cart = GetCartByUserId(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _db.Carts.Add(cart);
            _db.SaveChanges();
            cart = GetCartByUserId(userId)!;
        }
        return cart;
    }

    public CartItem? GetCartItem(int cartId, int productId) =>
        _db.CartItems.FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);

    public void SaveCartItem(CartItem item)
    {
        if (item.Id == 0) _db.CartItems.Add(item);
        else _db.CartItems.Update(item);
        _db.SaveChanges();
    }

    public void DeleteCartItem(int id)
    {
        var item = _db.CartItems.Find(id);
        if (item != null) { _db.CartItems.Remove(item); _db.SaveChanges(); }
    }

    public void ClearCart(int cartId)
    {
        var items = _db.CartItems.Where(i => i.CartId == cartId).ToList();
        _db.CartItems.RemoveRange(items);
        _db.SaveChanges();
    }
}
