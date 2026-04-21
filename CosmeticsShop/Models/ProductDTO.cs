namespace CosmeticsShop.Models;

public class ProductDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Volume { get; set; }
    public bool InStock { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsVerified { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public string PriceRangeLabel { get; set; } = string.Empty;
}
