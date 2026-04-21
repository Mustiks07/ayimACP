using System.ComponentModel.DataAnnotations;
using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Введите адрес доставки")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите номер телефона")]
    public string Phone { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public List<CartItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}
