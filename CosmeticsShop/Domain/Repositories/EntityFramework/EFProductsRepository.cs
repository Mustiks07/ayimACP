using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFProductsRepository : IProductsRepository
{
    private readonly AppDbContext _db;
    public EFProductsRepository(AppDbContext db) => _db = db;

    public IQueryable<Product> GetProducts() =>
        _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Reviews);

    public Product? GetProductById(int id) =>
        _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Reviews).ThenInclude(r => r.Author)
            .Include(p => p.Reviews).ThenInclude(r => r.Votes)
            .FirstOrDefault(p => p.Id == id);

    public void SaveProduct(Product product)
    {
        if (product.Id == 0)
        {
            product.DateCreated = DateTime.UtcNow;
            _db.Products.Add(product);
        }
        else
        {
            _db.Products.Update(product);
        }
        _db.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = _db.Products.Find(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}
