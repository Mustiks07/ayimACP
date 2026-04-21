namespace CosmeticsShop.Domain.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Country { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsVerified { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
