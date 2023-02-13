using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
namespace BulkyBookWeb.Controllers;

public class CategoryController: Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(){
        IEnumerable<Category> objCategoryList = _db.Categories.ToList();
        Console.WriteLine("hello");
        foreach (var item in objCategoryList)
        {  
            Console.WriteLine(item);
        }
        return View(objCategoryList);
    }
}
