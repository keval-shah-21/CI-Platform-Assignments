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

        return View(new IndexMissionVM()
        {
            missionVM = missionVM,
            cityVM = _unitOfService.Mission.GetCitiesByMission(missionVM),
            countryVM = _unitOfService.Mission.GetCountriesByMission(missionVM),
            skillVM = _unitOfService.Skill.GetAll(),
            missionThemeVM = _unitOfService.MissionTheme.GetAll()
        });
    }

    [HttpGet]
    [Route("filter-data")]
    public IActionResult FilterData(int? country, int[]? city, int[]? theme, int[]? skill, string? search, string? sort)
    {
       IndexMissionVM indexMissionVM = _unitOfService.Mission.FilterData(country, city, theme, skill, search, sort);
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