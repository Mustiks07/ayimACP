using CosmeticsShop.Domain.Enums;

namespace CosmeticsShop.Domain.Entities;

public class ReviewVote
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ReviewId { get; set; }
    public Review Review { get; set; } = null!;
    public ReviewVoteTypeEnum VoteType { get; set; }
}
