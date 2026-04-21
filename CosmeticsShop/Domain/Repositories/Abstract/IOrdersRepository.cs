using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface IOrdersRepository
{
    IQueryable<Order> GetOrders();
    Order? GetOrderById(int id);
    void SaveOrder(Order order);
    bool UserHasOrderedProduct(string userId, int productId);
}
