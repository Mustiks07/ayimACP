using CosmeticsShop.Domain.Entities;

namespace CosmeticsShop.Domain.Repositories.Abstract;

public interface IReviewsRepository
{
    IQueryable<Review> GetReviews();
    Review? GetReviewById(int id);
    void SaveReview(Review review);
    void DeleteReview(int id);
    ReviewVote? GetVote(int reviewId, string userId);
    void SaveVote(ReviewVote vote);
    void DeleteVote(int id);
}
