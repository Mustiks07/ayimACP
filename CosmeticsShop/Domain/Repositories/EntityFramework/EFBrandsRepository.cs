using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFBrandsRepository : IBrandsRepository
{
    private readonly AppDbContext _db;
    public EFBrandsRepository(AppDbContext db) => _db = db;

    public IQueryable<Brand> GetBrands() => _db.Brands;

    public Brand? GetBrandById(int id) => _db.Brands.Find(id);

    public void SaveBrand(Brand brand)
    {
        if (brand.Id == 0) _db.Brands.Add(brand);
        else _db.Brands.Update(brand);
        _db.SaveChanges();
    }

    public void DeleteBrand(int id)
    {
        var brand = _db.Brands.Find(id);
        if (brand != null) { _db.Brands.Remove(brand); _db.SaveChanges(); }
    }
}
