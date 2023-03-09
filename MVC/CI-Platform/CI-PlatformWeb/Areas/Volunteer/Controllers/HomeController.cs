﻿using System.Diagnostics;
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
    private List<SkillVM> skillVM = new();
    private List<MissionThemeVM> missionThemeVM = new();

    private List<MissionVM> missionVM = new();
    private List<MissionVM> filteredVM = new();

    IndexMissionVM indexMissionVM = new();

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
        missionVM = _unitOfService.Mission.GetAllIndexMission();
        filteredVM.AddRange(missionVM);

        GetAllCountryCityByMission(missionVM);
        skillVM = _unitOfService.Skill.GetAll();
        missionThemeVM = _unitOfService.MissionTheme.GetAll();

        indexMissionVM.missionVM = this.missionVM;
        indexMissionVM.cityVM = this.cityVM;
        indexMissionVM.countryVM = this.countryVM;
        indexMissionVM.missionThemeVM = this.missionThemeVM;
        indexMissionVM.skillVM = this.skillVM;
        return View(indexMissionVM);
    }

    [Route("search")]
    public IActionResult Search(string? query){
        Console.WriteLine("query... : "+query);
        if(query != "" && query != null){
            Console.WriteLine(missionVM.Count);
            filteredVM = filteredVM.Where(x => x.Title.Contains(query)).ToList();
            Console.WriteLine(filteredVM.Count);
            indexMissionVM.missionVM = filteredVM;
        }
        return PartialView("_IndexMissions", indexMissionVM);
    }

    [Route("sort")]
    public IActionResult Sort(string value){
        return View();
    }

    [Route("filter-city")]
    public IActionResult FilterCity(string city, long id){
        return View();
    }
    [Route("filter-country")]
    public IActionResult FilterCountry(string country, long id){
        Console.WriteLine("-----------------------inside ctroler");
        Console.WriteLine(indexMissionVM.cityVM.Count);
        // List<CityVM> filteredCity = indexMissionVM.cityVM.Where();
        return View();
    }
    [Route("filter-theme")]
    public IActionResult FilterTheme(string theme, long id){
        return View();
    }
    [Route("filter-skill")]
    public IActionResult FilterSkill(string skill, long id){
        return View();
    }

    [Route("update-header")]
    public IActionResult UpdateHeader(){
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

    public void GetAllCountryCityByMission(List<MissionVM> missionVM){
        List<CityVM> allCities = _unitOfService.City.GetAll();
        List<CountryVM> allCountries = _unitOfService.Country.GetAll();
        missionVM.ForEach(mission => {
            CityVM c = allCities.Where(all => all.CityName == mission.MissionCity).First();
            cityVM.Add(c);

            CountryVM co = allCountries.Where(all => all.CountryName == mission.MissionCountry).First();
            countryVM.Add(co);
        });
    }
}