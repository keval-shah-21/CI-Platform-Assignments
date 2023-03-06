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
    private HashSet<CityVM> cityVM = new();
    private HashSet<CountryVM> countryVM = new();
    private List<CityVM> allCities = new();
    private List<CountryVM> allCountries = new();
    private List<MissionApplicationVM> missionApplicationVM = new();
    private List<MissionVM> missionVM = new();
    private List<MissionSkillVM> missionSkillVM= new();
    private List<MissionThemeVM> missionThemeVM = new();
    private List<MissionGoalVM> missionGoalVM = new();
    private List<FavouriteMissionVM> favouriteMissionVM = new();
    private List<MissionMediaVM> missionMediaVM = new();

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IUnitOfService unitOfService)
    {
        _logger = logger;
        _unitOfService = unitOfService;
    }

    [Route("/", Name="Default")]
    [Route("index")]
    public IActionResult Index()
    {
        missionVM = _unitOfService.Mission.GetAll();
        GetMissionCountryCity(missionVM);
        missionMediaVM = _unitOfService.MissionMedia.GetAll();
        missionSkillVM = _unitOfService.MissionSkill.GetAll();
        missionThemeVM = _unitOfService.MissionTheme.GetAll();
        if(HttpContext.Session.GetString("UserId") != null){
            missionApplicationVM = _unitOfService.MissionApplication.GetAll();
            missionGoalVM = _unitOfService.MissionGoal.GetAll();
            favouriteMissionVM = _unitOfService.FavouriteMission.GetAll();
        }
        IndexMissionVM indexMissionVM = new IndexMissionVM(){
            missionVM = this.missionVM,
            cityVM = this.cityVM,
            countryVM = this.countryVM,
            missionApplicationVM = this.missionApplicationVM,
            missionMediaVM = this.missionMediaVM,
            missionGoalVM = this.missionGoalVM,
            favouriteMissionVM = this.favouriteMissionVM,
            missionSkillVM = this.missionSkillVM,
            missionThemeVM = this.missionThemeVM
        };
        return View(indexMissionVM);
    }

    // [Route("search")]
    // public IActionResult Search(string? query){
    //     IList<string> filtered;
    //     if(query != "" && query != null){
    //         filtered = myModel.Where(x => x.Contains(query)).ToList();
    //         return PartialView("_IndexMissions", filtered);
    //     }
    //     return PartialView("_IndexMissions", myModel);
    // }

    [Route("sort")]
    public IActionResult Sort(string value){
        return View();
    }

    [Route("filter-city")]
    public IActionResult FilterCity(string city){
        return View();
    }
    [Route("filter-country")]
    public IActionResult FilterCountry(string country){
        return View();
    }
    [Route("filter-theme")]
    public IActionResult FilterTheme(string theme){
        return View();
    }
    [Route("filter-skill")]
    public IActionResult FilterSkill(string skill){
        return View();
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

    public void GetMissionCountryCity(List<MissionVM> missionVM){
        allCities = _unitOfService.City.GetAll();
        allCountries = _unitOfService.Country.GetAll();
        missionVM.ForEach(mission => {
            CityVM c = allCities.Where(all => all.CityId == mission.MissionCity).First();
            cityVM.Add(c);
            CountryVM co = allCountries.Where(all => all.CountryId== mission.MissionCountry).First();
            countryVM.Add(co);
        });
    }
}