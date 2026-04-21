using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface ICategoriesRepository
{
    IQueryable<Category> GetCategories();
    Category? GetCategoryById(int id);
    void SaveCategory(Category category);
    void DeleteCategory(int id);
}
