using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public string StatusLabel { get; set; } = string.Empty;
    public string StatusColor { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public decimal Total { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}
