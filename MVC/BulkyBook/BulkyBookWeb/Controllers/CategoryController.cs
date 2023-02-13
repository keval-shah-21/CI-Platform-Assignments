using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Controllers;
public class CategoryController: Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }


    public IActionResult Index(){
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }

    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category){
        if(ModelState.IsValid){
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully!";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int? id){
        if(id == null || id == 0)
            return NotFound();
        var category = _db.Categories.Find(id);
        if(category == null)
            return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category){
        if(ModelState.IsValid){
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully!";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Delete(int? id){
        if(id == null || id == 0)
            return NotFound();
        var category = _db.Categories.Find(id);
        if(category == null)
            return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id){
        var category = _db.Categories.Find(id);
        if(category == null)
            return NotFound();
        _db.Remove(category);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully!";
        return RedirectToAction("Index");
    }
}
