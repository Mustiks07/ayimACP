using System.ComponentModel.DataAnnotations;

namespace CosmeticsShop.Models;

public class CreateReviewViewModel
{
    [Required]
    public int ProductId { get; set; }

    public string ProductTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите заголовок")]
    public string Title { get; set; } = string.Empty;

    public string? Text { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    [Range(1, 5)]
    public int QualityRating { get; set; } = 5;

    [Range(1, 5)]
    public int PackagingRating { get; set; } = 5;

    [Range(1, 5)]
    public int ValueRating { get; set; } = 5;
}
