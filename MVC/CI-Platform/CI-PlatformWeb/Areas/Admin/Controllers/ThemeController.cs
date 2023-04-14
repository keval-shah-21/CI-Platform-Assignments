using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ThemeController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public ThemeController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadThemePage()
    {
        try
        {
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.GetAll();
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult AddTheme()
    {
        try
        {
            return PartialView("_AddTheme");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading addTheme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPost]
    public IActionResult AddTheme(MissionThemeVM mt)
    {
        try
        {
            _unitOfService.MissionTheme.SaveTheme(mt);
            _unitOfService.Save();
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.GetAll();
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading addTheme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult EditTheme(long id)
    {
        try
        {
            return PartialView("_EditTheme", _unitOfService.MissionTheme.GetThemeById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading editTheme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPut]
    public IActionResult EditTheme(MissionThemeVM mt)
    {
        try
        {
            _unitOfService.MissionTheme.UpdateTheme(mt);
            _unitOfService.Save();
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.GetAll();
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating Theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteTheme(long id)
    {
        try
        {
            if (_unitOfService.MissionTheme.IsAlreadyUsed(id))
            {
                return NoContent();
            }
            _unitOfService.MissionTheme.DeleteTheme(id);
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.GetAll();
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting Theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult DeactivateTheme(long id, int value)
    {
        try
        {
            _unitOfService.MissionTheme.UpdateStatusByid(id, value);
            _unitOfService.Save();
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.GetAll();
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating Theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult SearchTheme(string? query)
    {
        try
        {
            List<MissionThemeVM> themes = _unitOfService.MissionTheme.Search(query);
            return PartialView("_Theme", themes);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching theme: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public bool IsThemeUnique(string themeName, long? id)
    {
        if (string.IsNullOrEmpty(themeName)) return false;
        return _unitOfService.MissionTheme.IsThemeUnique(themeName, id);
    }
}
