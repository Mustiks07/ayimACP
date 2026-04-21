using Microsoft.AspNetCore.Identity;

namespace CosmeticsShop.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = null!;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal Total => Items.Sum(i => (i.Product?.Price ?? 0) * i.Quantity);
}
