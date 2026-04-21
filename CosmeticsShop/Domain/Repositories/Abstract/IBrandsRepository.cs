using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface IBrandsRepository
{
    IQueryable<Brand> GetBrands();
    Brand? GetBrandById(int id);
    void SaveBrand(Brand brand);
    void DeleteBrand(int id);
}
