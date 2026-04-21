using System.ComponentModel.DataAnnotations;
using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Enums;

namespace CosmeticsShop.Models;

public class CreateProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Введите цену")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }
    public string? Volume { get; set; }
    public string? SkinType { get; set; }
    public bool InStock { get; set; } = true;
    public bool IsBestseller { get; set; }
    public bool IsVerified { get; set; }

    [Required(ErrorMessage = "Выберите бренд")]
    public int BrandId { get; set; }

    [Required(ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }

    public PriceRangeEnum PriceRange { get; set; }

    public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
}
