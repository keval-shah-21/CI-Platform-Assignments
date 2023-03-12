using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CI_PlatformWeb.Models;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/volunteer/home/")]
public class HomeController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IUnitOfService unitOfService)
    {
        _logger = logger;
        _unitOfService = unitOfService;
    }

    [Route("/", Name = "Default")]
    [Route("index")]
    public IActionResult Index()
    {
        List<MissionVM> missionVM = _unitOfService.Mission.GetAllIndexMission();
        @ViewBag.TotalMissions = missionVM?.Count();
        return View(new IndexMissionVM()
        {
            missionVM = missionVM.Take(9).ToList(),
            cityVM = _unitOfService.Mission.GetCitiesByMission(missionVM),
            countryVM = _unitOfService.Mission.GetCountriesByMission(missionVM),
            skillVM = _unitOfService.Skill.GetAll(),
            missionThemeVM = _unitOfService.MissionTheme.GetAll()
        });
    }

    [Route("filter-data")]
    public IActionResult FilterData(int? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, int page)
    {
        long? userId = 0;
        if(HttpContext.Session.GetString("UserId") != null){
            userId = long.Parse(HttpContext.Session.GetString("UserId"));
        }
        IndexMissionVM indexMissionVM = _unitOfService.Mission.FilterData(country, city, theme, skill, search, sort, userId);
        @ViewBag.TotalMissions = indexMissionVM.missionVM?.Count();
        indexMissionVM.missionVM = indexMissionVM.missionVM.Skip( (page - 1) * 9 ).Take(9).ToList();
        return PartialView("_IndexMissions", indexMissionVM);
    }

    [Route("privacy")]
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