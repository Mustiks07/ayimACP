using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsShop.Domain.Repositories.EntityFramework;

public class EFReviewsRepository : IReviewsRepository
{
    private readonly AppDbContext _db;
    public EFReviewsRepository(AppDbContext db) => _db = db;

    public IQueryable<Review> GetReviews() =>
        _db.Reviews
            .Include(r => r.Author)
            .Include(r => r.Product)
            .Include(r => r.Votes);

    public Review? GetReviewById(int id) =>
        _db.Reviews
            .Include(r => r.Author)
            .Include(r => r.Product)
            .Include(r => r.Votes)
            .FirstOrDefault(r => r.Id == id);

    public void SaveReview(Review review)
    {
        if (review.Id == 0)
        {
            review.DateCreated = DateTime.UtcNow;
            _db.Reviews.Add(review);
        }
        else
        {
            _db.Reviews.Update(review);
        }
        _db.SaveChanges();
    }

    public void DeleteReview(int id)
    {
        var review = _db.Reviews.Find(id);
        if (review != null) { _db.Reviews.Remove(review); _db.SaveChanges(); }
    }

    public ReviewVote? GetVote(int reviewId, string userId) =>
        _db.ReviewVotes.FirstOrDefault(v => v.ReviewId == reviewId && v.UserId == userId);

    public void SaveVote(ReviewVote vote)
    {
        if (vote.Id == 0) _db.ReviewVotes.Add(vote);
        else _db.ReviewVotes.Update(vote);
        _db.SaveChanges();
    }

    public void DeleteVote(int id)
    {
        var vote = _db.ReviewVotes.Find(id);
        if (vote != null) { _db.ReviewVotes.Remove(vote); _db.SaveChanges(); }
    }
}
