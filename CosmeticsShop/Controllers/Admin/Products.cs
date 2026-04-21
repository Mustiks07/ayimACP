using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Infrastructure;
using CosmeticsShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminProductsController : Controller
{
    private readonly DataManager _data;
    public AdminProductsController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var products = _data.Products.GetProducts().ToList();
        return View("~/Views/Admin/Products/Index.cshtml", products);
    }

    [HttpGet]
    public IActionResult New()
    {
        var model = new CreateProductViewModel
        {
            Brands     = _data.Brands.GetBrands().ToList(),
            Categories = _data.Categories.GetCategories().ToList()
        };
        return View("~/Views/Admin/Products/Form.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult New(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Brands     = _data.Brands.GetBrands().ToList();
            model.Categories = _data.Categories.GetCategories().ToList();
            return View("~/Views/Admin/Products/Form.cshtml", model);
        }

        var product = new Product
        {
            Title       = model.Title,
            Description = model.Description,
            Price       = model.Price,
            ImageUrl    = model.ImageUrl,
            Volume      = model.Volume,
            SkinType    = model.SkinType,
            InStock     = model.InStock,
            IsBestseller = model.IsBestseller,
            IsVerified  = model.IsVerified,
            BrandId     = model.BrandId,
            CategoryId  = model.CategoryId,
            PriceRange  = model.PriceRange
        };

        _data.Products.SaveProduct(product);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _data.Products.GetProductById(id);
        if (product == null) return NotFound();

        var model = new CreateProductViewModel
        {
            Id          = product.Id,
            Title       = product.Title,
            Description = product.Description,
            Price       = product.Price,
            ImageUrl    = product.ImageUrl,
            Volume      = product.Volume,
            SkinType    = product.SkinType,
            InStock     = product.InStock,
            IsBestseller = product.IsBestseller,
            IsVerified  = product.IsVerified,
            BrandId     = product.BrandId,
            CategoryId  = product.CategoryId,
            PriceRange  = product.PriceRange,
            Brands      = _data.Brands.GetBrands().ToList(),
            Categories  = _data.Categories.GetCategories().ToList()
        };
        return View("~/Views/Admin/Products/Form.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Brands     = _data.Brands.GetBrands().ToList();
            model.Categories = _data.Categories.GetCategories().ToList();
            return View("~/Views/Admin/Products/Form.cshtml", model);
        }

        var product = _data.Products.GetProductById(model.Id);
        if (product == null) return NotFound();

        product.Title       = model.Title;
        product.Description = model.Description;
        product.Price       = model.Price;
        product.ImageUrl    = model.ImageUrl;
        product.Volume      = model.Volume;
        product.SkinType    = model.SkinType;
        product.InStock     = model.InStock;
        product.IsBestseller = model.IsBestseller;
        product.IsVerified  = model.IsVerified;
        product.BrandId     = model.BrandId;
        product.CategoryId  = model.CategoryId;
        product.PriceRange  = model.PriceRange;

        _data.Products.SaveProduct(product);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _data.Products.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }
}
