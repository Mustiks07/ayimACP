using CosmeticsShop.Domain.Enums;
using CosmeticsShop.Infrastructure;
using CosmeticsShop.Models;
using CosmeticsShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

public class ProductsController : Controller
{
    private readonly DataManager _data;
    private readonly LangService _lang;

    public ProductsController(DataManager data, LangService lang)
    {
        _data = data;
        _lang = lang;
    }

    public IActionResult Index(ProductFilterViewModel filter)
    {
        var query = _data.Products.GetProducts().AsEnumerable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(p => p.Title.Contains(filter.Search, StringComparison.OrdinalIgnoreCase)
                                  || (p.Description ?? "").Contains(filter.Search, StringComparison.OrdinalIgnoreCase));

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

        if (filter.BrandId.HasValue)
            query = query.Where(p => p.BrandId == filter.BrandId.Value);

        if (filter.PriceRange.HasValue)
            query = query.Where(p => p.PriceRange == filter.PriceRange.Value);

        if (!string.IsNullOrWhiteSpace(filter.SkinType))
            query = query.Where(p => (p.SkinType ?? "").Contains(filter.SkinType, StringComparison.OrdinalIgnoreCase));

        if (filter.InStockOnly)
            query = query.Where(p => p.InStock);

        query = filter.Sort switch
        {
            "rating"     => query.OrderByDescending(p => p.AverageRating),
            "price_asc"  => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _            => query.OrderByDescending(p => p.DateCreated)
        };

        filter.Products = query.Select(p => new ProductDTO
        {
            Id             = p.Id,
            Title          = p.Title,
            BrandName      = p.Brand.Title,
            CategoryName   = p.Category.Title,
            Price          = p.Price,
            ImageUrl       = p.ImageUrl,
            Volume         = p.Volume,
            InStock        = p.InStock,
            IsBestseller   = p.IsBestseller,
            IsVerified     = p.IsVerified,
            AverageRating  = p.AverageRating,
            ReviewCount    = p.ReviewCount,
            PriceRangeLabel = GetPriceLabel(p.PriceRange, _lang)
        }).ToList();

        filter.Brands     = _data.Brands.GetBrands().ToList();
        filter.Categories = _data.Categories.GetCategories().ToList();

        return View(filter);
    }

    public IActionResult Show(int id)
    {
        var product = _data.Products.GetProductById(id);
        if (product == null) return NotFound();
        return View(product);
    }

    private static string GetPriceLabel(PriceRangeEnum range, LangService lang) => range switch
    {
        PriceRangeEnum.Budget  => lang.T("price.budget"),
        PriceRangeEnum.Medium  => lang.T("price.medium"),
        PriceRangeEnum.Premium => lang.T("price.premium"),
        PriceRangeEnum.Luxury  => lang.T("price.luxury"),
        _                      => ""
    };
}
