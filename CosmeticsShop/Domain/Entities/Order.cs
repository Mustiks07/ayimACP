using CosmeticsShop.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CosmeticsShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = null!;
    public OrderStatusEnum Status { get; set; }
    public DateTime DateCreated { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public decimal Total { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
