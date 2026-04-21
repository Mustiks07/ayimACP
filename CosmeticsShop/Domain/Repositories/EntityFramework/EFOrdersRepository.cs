using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFOrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _db;
    public EFOrdersRepository(AppDbContext db) => _db = db;

    public IQueryable<Order> GetOrders() =>
        _db.Orders
            .Include(o => o.User)
            .Include(o => o.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Brand);

    public Order? GetOrderById(int id) =>
        _db.Orders
            .Include(o => o.User)
            .Include(o => o.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Brand)
            .FirstOrDefault(o => o.Id == id);

    public void SaveOrder(Order order)
    {
        if (order.Id == 0)
        {
            order.DateCreated = DateTime.UtcNow;
            _db.Orders.Add(order);
        }
        else
        {
            _db.Orders.Update(order);
        }
        _db.SaveChanges();
    }

    public bool UserHasOrderedProduct(string userId, int productId) =>
        _db.Orders
            .Where(o => o.UserId == userId)
            .Any(o => o.Items.Any(i => i.ProductId == productId));
}
