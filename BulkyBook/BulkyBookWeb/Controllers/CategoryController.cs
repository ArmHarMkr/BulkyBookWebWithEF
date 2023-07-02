using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        this._db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategory = _db.Categories.ToList();
        return View(objCategory);
    }

    //GET
    [HttpGet]
    public IActionResult Create()
    {
        
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }


    //GET EDIT
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb == null)
            return NotFound();
        return View(categoryFromDb);
    }

    public IActionResult Chgitem(string? name)
    {
        return View(name);
    }


    //POST EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if(category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    //GET Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if(id != null)
        {
            Category? category = await _db.Categories.FindAsync(id);
            if(category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }
}