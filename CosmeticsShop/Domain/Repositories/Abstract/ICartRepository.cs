using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface ICartRepository
{
    Cart? GetCartByUserId(string userId);
    Cart GetOrCreateCart(string userId);
    CartItem? GetCartItem(int cartId, int productId);
    void SaveCartItem(CartItem item);
    void DeleteCartItem(int id);
    void ClearCart(int cartId);
}
