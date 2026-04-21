using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface IProductsRepository
{
    IQueryable<Product> GetProducts();
    Product? GetProductById(int id);
    void SaveProduct(Product product);
    void DeleteProduct(int id);
}
