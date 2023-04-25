using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CI_PlatformWeb.Models;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_PlatformWeb.Areas.Volunteer.Utilities;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[AuthenticateAdmin]
public class HomeController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IUnitOfService unitOfService)
    {
        _logger = logger;
        _unitOfService = unitOfService;
    }

    public IActionResult Index(string? profileSuccess, string? registered)
    {
        List<IndexMissionVM> missionVM = _unitOfService.Mission.GetAllIndexMissions();
        ViewBag.ProfileSuccess = profileSuccess;
        ViewBag.Registered = registered;
        return View(new IndexHeaderVM()
        {
            cityVM = _unitOfService.City.GetCitiesByMissions(missionVM),
            countryVM = _unitOfService.Country.GetCountriesByMissions(missionVM),
            skillVM = _unitOfService.Skill.GetAll(),
            missionThemeVM = _unitOfService.MissionTheme.GetAll()
        });
    }

    [HttpPost]
    public IActionResult FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, int page)
    {
        long? userId = 0;
        if(HttpContext.Session.GetString("UserId") != null){
            userId = long.Parse(HttpContext.Session.GetString("UserId"));
        }
        List<IndexMissionVM> missionVMs = _unitOfService.Mission.FilterMissions(country, city, theme, skill, search, sort, userId);
        ViewBag.TotalMissions = missionVMs.LongCount();
        missionVMs = missionVMs.Skip((page - 1) * 9).Take(9).ToList();
        return PartialView("_IndexMissions", missionVMs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}