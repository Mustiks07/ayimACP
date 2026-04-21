using CosmeticsShop.Infrastructure;
using CosmeticsShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers;

public class HomeController : Controller
{
    private readonly DataManager _data;
    private readonly LangService _lang;

    public HomeController(DataManager data, LangService lang)
    {
        _data = data;
        _lang = lang;
    }

    public IActionResult Index()
    {
        var products = _data.Products.GetProducts().ToList();
        var bestsellers = products
            .Where(p => p.IsBestseller && p.InStock)
            .Take(4)
            .ToList();

        var categories = _data.Categories.GetCategories().ToList();
        var brands = _data.Brands.GetBrands().ToList();

        double avgRating = 0;
        if (products.Any(p => p.Reviews.Count > 0))
        {
            var allRatings = products.SelectMany(p => p.Reviews).Select(r => (double)r.Rating).ToList();
            if (allRatings.Count > 0) avgRating = allRatings.Average();
        }

        ViewBag.Bestsellers = bestsellers;
        ViewBag.Categories = categories;
        ViewBag.Brands = brands;
        ViewBag.TotalProducts = products.Count;
        ViewBag.TotalBrands = brands.Count;
        ViewBag.AvgRating = Math.Round(avgRating, 1);

        return View();
    }

    public IActionResult Top()
    {
        var products = _data.Products.GetProducts().ToList()
            .Where(p => p.Reviews.Count > 0)
            .OrderByDescending(p => p.AverageRating)
            .ThenByDescending(p => p.ReviewCount)
            .Take(20)
            .ToList();

        return View(products);
    }

    public IActionResult Brands()
    {
        var brands = _data.Brands.GetBrands().ToList();
        return View(brands);
    }

    public IActionResult BrandProducts(int id)
    {
        var brand = _data.Brands.GetBrandById(id);
        if (brand == null) return NotFound();

        var products = _data.Products.GetProducts()
            .Where(p => p.BrandId == id)
            .ToList();

        ViewBag.Brand = brand;
        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
