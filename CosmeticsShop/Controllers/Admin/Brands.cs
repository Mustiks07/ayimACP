using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminBrandsController : Controller
{
    private readonly DataManager _data;
    public AdminBrandsController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var brands = _data.Brands.GetBrands().ToList();
        return View("~/Views/Admin/Brands/Index.cshtml", brands);
    }

    [HttpGet]
    public IActionResult New() => View("~/Views/Admin/Brands/Form.cshtml", new Brand());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult New(Brand brand)
    {
        if (!ModelState.IsValid) return View("~/Views/Admin/Brands/Form.cshtml", brand);
        _data.Brands.SaveBrand(brand);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var brand = _data.Brands.GetBrandById(id);
        if (brand == null) return NotFound();
        return View("~/Views/Admin/Brands/Form.cshtml", brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Brand brand)
    {
        if (!ModelState.IsValid) return View("~/Views/Admin/Brands/Form.cshtml", brand);
        _data.Brands.SaveBrand(brand);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _data.Brands.DeleteBrand(id);
        return RedirectToAction(nameof(Index));
    }
}
