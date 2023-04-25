using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class MissionController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MissionController(IUnitOfService unitOfService, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfService = unitOfService;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult LoadMissionPage() => PartialView("_Mission", _unitOfService.Mission.GetAllAdminMission().OrderByDescending(m => m.CreatedAt));

    public IActionResult AddTimeMission()
    {
        try
        {
            TimeMissionVM time = new TimeMissionVM()
            {
                CityVMs = _unitOfService.City.GetAll(),
                CountryVMs = _unitOfService.Country.GetAll(),
                ThemeVMs = _unitOfService.MissionTheme.GetAll().Where(m => m.Status == true).ToList(),
                SkillVMs = _unitOfService.Skill.GetAll().Where(s => s.Status == true).ToList(),
            };
            return PartialView("_AddTimeMission", time);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    public IActionResult AddGoalMission()
    {
        try
        {
            GoalMissionVM time = new GoalMissionVM()
            {
                CityVMs = _unitOfService.City.GetAll(),
                CountryVMs = _unitOfService.Country.GetAll(),
                ThemeVMs = _unitOfService.MissionTheme.GetAll().Where(m => m.Status == true).ToList(),
                SkillVMs = _unitOfService.Skill.GetAll().Where(s => s.Status == true).ToList(),
            };
            return PartialView("_AddGoalMission", time);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills)
    {
        try
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _unitOfService.Mission.AddTimeMission(time, ImagesInput, DocumentsInput, MissionSkills, wwwRootPath);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills)
    {
        try
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _unitOfService.Mission.AddGoalMission(goal, ImagesInput, DocumentsInput, MissionSkills, wwwRootPath);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    public IActionResult EditTimeMission(long id)
    {
        try
        {
            TimeMissionVM time = _unitOfService.Mission.GetTimeMissionById(id);
            time.CityVMs = _unitOfService.City.GetAll();
            time.CountryVMs = _unitOfService.Country.GetAll();
            time.ThemeVMs = _unitOfService.MissionTheme.GetAll();
            time.SkillVMs = _unitOfService.Skill.GetAll();
            return PartialView("_EditTimeMission", time);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    public IActionResult EditGoalMission(long id)
    {
        try
        {
            GoalMissionVM time = _unitOfService.Mission.GetGoalMissionById(id);
            time.CityVMs = _unitOfService.City.GetAll();
            time.CountryVMs = _unitOfService.Country.GetAll();
            time.ThemeVMs = _unitOfService.MissionTheme.GetAll();
            time.SkillVMs = _unitOfService.Skill.GetAll();

            return PartialView("_EditGoalMission", time);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> EditTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills)
    {
        try
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _unitOfService.Mission.UpdateTimeMission(time, ImagesInput, DocumentsInput, MissionSkills, preLoadedImages, preLoadedDocs, preLoadedSkills, wwwRootPath);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> EditGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills)
    {
        try
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            await _unitOfService.Mission.UpdateGoalMission(goal, ImagesInput, DocumentsInput, MissionSkills, preLoadedImages, preLoadedDocs, preLoadedSkills, wwwRootPath);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    public IActionResult UpdateStatus(long id, int value)
    {
        try
        {
            _unitOfService.Mission.UpdateStatus(id, value);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    public IActionResult SearchMission(string? query) => PartialView("_Mission", _unitOfService.Mission.Search(query).OrderByDescending(m => m.CreatedAt));
}
