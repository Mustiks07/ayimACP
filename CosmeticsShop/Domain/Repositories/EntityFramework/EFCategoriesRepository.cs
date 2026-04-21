using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFCategoriesRepository : ICategoriesRepository
{
    private readonly AppDbContext _db;
    public EFCategoriesRepository(AppDbContext db) => _db = db;

    public IQueryable<Category> GetCategories() => _db.Categories;

    public Category? GetCategoryById(int id) => _db.Categories.Find(id);

    public void SaveCategory(Category category)
    {
        if (category.Id == 0) _db.Categories.Add(category);
        else _db.Categories.Update(category);
        _db.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var cat = _db.Categories.Find(id);
        if (cat != null) { _db.Categories.Remove(cat); _db.SaveChanges(); }
    }
}
