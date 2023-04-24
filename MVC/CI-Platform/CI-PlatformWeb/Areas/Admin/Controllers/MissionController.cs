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
                ThemeVMs = _unitOfService.MissionTheme.GetAll(),
                SkillVMs = _unitOfService.Skill.GetAll()
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
                ThemeVMs = _unitOfService.MissionTheme.GetAll(),
                SkillVMs = _unitOfService.Skill.GetAll()
            };
            return PartialView("_AddGoalMission", time);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public IActionResult AddTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills)
    {
        try
        {
            long id = _unitOfService.Mission.AddTimeMission(time);
            AddMedia(ImagesInput, id);
            AddDocuments(DocumentsInput, id);
            AddMissionSkill(MissionSkills, id);
            _unitOfService.Save();
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    [HttpPost]
    public IActionResult AddGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills)
    {
        try
        {
            long id = _unitOfService.Mission.AddGoalMission(goal);
            _unitOfService.MissionGoal.AddMissionGoal(new MissionGoalVM()
            {
                GoalObjective = goal.GoalObjective,
                GoalValue = goal.GoalValue,
                MissionId = id
            });
            AddMedia(ImagesInput, id);
            AddDocuments(DocumentsInput, id);
            AddMissionSkill(MissionSkills, id);
            _unitOfService.Save();
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
    public IActionResult EditTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills)
    {
        try
        {
            long id = time.MissionId;
            _unitOfService.Mission.UpdateTimeMission(time);
            EditMedia(ImagesInput, preLoadedImages, id);
            EditDocuments(DocumentsInput, preLoadedDocs, id);
            if(!MissionSkills.SequenceEqual(preLoadedSkills))
                EditMissionSkill(MissionSkills, preLoadedSkills, id);
            _unitOfService.Save();
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult EditGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills)
    {
        try
        {
            long id = goal.MissionId;
            _unitOfService.Mission.UpdateGoalMission(goal);
            _unitOfService.MissionGoal.UpdateMissionGoal(new MissionGoalVM()
            {
                GoalObjective = goal.GoalObjective,
                GoalValue = goal.GoalValue,
                MissionGoalId = goal.MissionGoalId
            });
            EditMedia(ImagesInput, preLoadedImages, id);
            EditDocuments(DocumentsInput, preLoadedDocs, id);
            if (!MissionSkills.SequenceEqual(preLoadedSkills))
                EditMissionSkill(MissionSkills, preLoadedSkills, id);
            _unitOfService.Save();
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

    [NonAction]
    private void AddMedia(List<IFormFile> images, long missionId)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        _unitOfService.MissionMedia.AddMissionMedia(wwwRootPath, images, missionId);
    }
    [NonAction]
    private void AddDocuments(List<IFormFile> docs, long missionId)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        _unitOfService.MissionDocument.AddMissionDocuments(wwwRootPath, docs, missionId);
    }
    [NonAction]
    private void AddMissionSkill(List<string> skills, long missionId)
    {
        _unitOfService.MissionSkill.AddMissionSkill(skills, missionId);
    }
    [NonAction]
    private void EditMedia(List<IFormFile> images, List<string> preLoaded, long missionId)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        _unitOfService.MissionMedia.EditMissionMedia(wwwRootPath, images, missionId, preLoaded);
    }
    [NonAction]
    private void EditDocuments(List<IFormFile> docs, List<string> preLoaded, long missionId)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        _unitOfService.MissionDocument.EditMissionDocuments(wwwRootPath, docs, missionId, preLoaded);
    }
    [NonAction]
    private void EditMissionSkill(List<string> skills, List<string> preLoaded, long missionId)
    {
        _unitOfService.MissionSkill.EditMissionSkill(skills, preLoaded, missionId);
    }
}
