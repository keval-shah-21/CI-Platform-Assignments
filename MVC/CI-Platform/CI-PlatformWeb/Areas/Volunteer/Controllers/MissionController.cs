using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
public class MissionController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public MissionController(IUnitOfService unitOfService)
    {
            _unitOfService = unitOfService;
    }

    public IActionResult MissionDetails(long? id)
    {
        if (id == 0) return NotFound();
        MissionVM missionVM = _unitOfService.Mission.GetMissionById(id);
        if (missionVM == null) return NotFound();
        return View(missionVM);
    }

    public IActionResult RelatedMissions(long id){
        List<MissionVM> missionVM = _unitOfService.Mission.GetRelatedMissions(id);
        missionVM = missionVM.Take(3).ToList();
        ViewBag.TotalMissions = missionVM.Count();
        return PartialView("../Home/_IndexMissions", missionVM);
    }

    [HttpPost]
    public IActionResult PostComment(long missionId, long userId, string comment){
        _unitOfService.Comment.PostComment(missionId, userId, comment);
        _unitOfService.Save();
        MissionVM mission = _unitOfService.Mission.GetMissionById(missionId);
        return PartialView("_MissionComments", mission);
    }

    [HttpPost]
    public IActionResult RateMission(long missionId, long userId, byte rate){ 
        _unitOfService.MissionRating.RateMission(missionId, userId, rate);
         MissionVM missionVM = _unitOfService.Mission.UpdateMissionRating(missionId);
        _unitOfService.Save();
        return PartialView("_MissionRating", missionVM);
    }
}