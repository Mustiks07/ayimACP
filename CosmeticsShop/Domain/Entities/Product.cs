using CosmeticsShop.Domain.Enums;

namespace CosmeticsShop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Volume { get; set; }
    public string? SkinType { get; set; }
    public bool InStock { get; set; }
    public bool IsVerified { get; set; }
    public bool IsBestseller { get; set; }
    public PriceRangeEnum PriceRange { get; set; }
    public DateTime DateCreated { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public double AverageRating => Reviews.Count > 0
        ? Reviews.Average(r => r.Rating)
        : 0;

    public int ReviewCount => Reviews.Count;
}
