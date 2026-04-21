using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Enums;

namespace CosmeticsShop.Models;

public class ProductFilterViewModel
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public PriceRangeEnum? PriceRange { get; set; }
    public string? SkinType { get; set; }
    public string? Sort { get; set; }
    public bool InStockOnly { get; set; }

    public List<ProductDTO> Products { get; set; } = new();
    public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
}
