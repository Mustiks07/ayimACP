using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Enums;
using CosmeticsShop.Infrastructure;
using CosmeticsShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly DataManager _data;
    private readonly UserManager<IdentityUser> _userManager;

    public ReviewsController(DataManager data, UserManager<IdentityUser> userManager)
    {
        _data = data;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult New(int productId)
    {
        var product = _data.Products.GetProductById(productId);
        if (product == null) return NotFound();

        var userId = _userManager.GetUserId(User)!;
        var existing = _data.Reviews.GetReviews()
            .FirstOrDefault(r => r.ProductId == productId && r.AuthorId == userId);

        if (existing != null)
        {
            TempData["Error"] = "Вы уже оставили отзыв на этот товар";
            return RedirectToAction("Show", "Products", new { id = productId });
        }

        return View(new CreateReviewViewModel { ProductId = productId, ProductTitle = product.Title });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult New(CreateReviewViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var userId = _userManager.GetUserId(User)!;
        var verifiedPurchase = _data.Orders.UserHasOrderedProduct(userId, model.ProductId);

        var review = new Review
        {
            Title           = model.Title,
            Text            = model.Text,
            Rating          = model.Rating,
            QualityRating   = model.QualityRating,
            PackagingRating = model.PackagingRating,
            ValueRating     = model.ValueRating,
            ProductId       = model.ProductId,
            AuthorId        = userId,
            VerifiedPurchase = verifiedPurchase
        };

        _data.Reviews.SaveReview(review);
        TempData["Success"] = "Отзыв опубликован";
        return RedirectToAction("Show", "Products", new { id = model.ProductId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Vote(int reviewId, ReviewVoteTypeEnum voteType)
    {
        var userId = _userManager.GetUserId(User)!;
        var existingVote = _data.Reviews.GetVote(reviewId, userId);

        if (existingVote != null)
        {
            if (existingVote.VoteType == voteType)
                _data.Reviews.DeleteVote(existingVote.Id);
            else
            {
                existingVote.VoteType = voteType;
                _data.Reviews.SaveVote(existingVote);
            }
        }
        else
        {
            _data.Reviews.SaveVote(new ReviewVote
            {
                ReviewId = reviewId,
                UserId   = userId,
                VoteType = voteType
            });
        }

        var review = _data.Reviews.GetReviewById(reviewId);
        return RedirectToAction("Show", "Products", new { id = review?.ProductId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var review = _data.Reviews.GetReviewById(id);
        if (review == null) return NotFound();

        var isAdmin = User.IsInRole("Admin");
        if (review.AuthorId != userId && !isAdmin) return Forbid();

        var productId = review.ProductId;
        _data.Reviews.DeleteReview(id);
        return RedirectToAction("Show", "Products", new { id = productId });
    }
}
