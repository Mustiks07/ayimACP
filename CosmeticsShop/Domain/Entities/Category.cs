namespace CosmeticsShop.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? IconEmoji { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
