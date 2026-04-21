using CosmeticsShop.Domain.Repositories.Abstract;

namespace CosmeticsShop.Infrastructure;

public class DataManager
{
    public IProductsRepository Products { get; }
    public IBrandsRepository Brands { get; }
    public ICategoriesRepository Categories { get; }
    public IReviewsRepository Reviews { get; }
    public ICartRepository Cart { get; }
    public IOrdersRepository Orders { get; }

    public DataManager(
        IProductsRepository products,
        IBrandsRepository brands,
        ICategoriesRepository categories,
        IReviewsRepository reviews,
        ICartRepository cart,
        IOrdersRepository orders)
    {
        Products = products;
        Brands = brands;
        Categories = categories;
        Reviews = reviews;
        Cart = cart;
        Orders = orders;
    }
}
