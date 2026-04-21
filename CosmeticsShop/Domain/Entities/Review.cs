using Microsoft.AspNetCore.Identity;

namespace CosmeticsShop.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Text { get; set; }
    public int Rating { get; set; }
    public int QualityRating { get; set; }
    public int PackagingRating { get; set; }
    public int ValueRating { get; set; }
    public bool VerifiedPurchase { get; set; }
    public DateTime DateCreated { get; set; }

    public string AuthorId { get; set; } = string.Empty;
    public IdentityUser Author { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<ReviewVote> Votes { get; set; } = new List<ReviewVote>();
}
