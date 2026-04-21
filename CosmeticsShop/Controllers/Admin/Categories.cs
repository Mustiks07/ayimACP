using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsShop.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminCategoriesController : Controller
{
    private readonly DataManager _data;
    public AdminCategoriesController(DataManager data) => _data = data;

    public IActionResult Index()
    {
        var categories = _data.Categories.GetCategories().ToList();
        return View("~/Views/Admin/Categories/Index.cshtml", categories);
    }

    [HttpGet]
    public IActionResult New() => View("~/Views/Admin/Categories/Form.cshtml", new Category());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult New(Category category)
    {
        if (!ModelState.IsValid) return View("~/Views/Admin/Categories/Form.cshtml", category);
        _data.Categories.SaveCategory(category);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var cat = _data.Categories.GetCategoryById(id);
        if (cat == null) return NotFound();
        return View("~/Views/Admin/Categories/Form.cshtml", cat);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View("~/Views/Admin/Categories/Form.cshtml", category);
        _data.Categories.SaveCategory(category);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _data.Categories.DeleteCategory(id);
        return RedirectToAction(nameof(Index));
    }
}
