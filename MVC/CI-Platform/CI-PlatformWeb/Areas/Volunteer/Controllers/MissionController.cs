using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using CI_PlatformWeb.Areas.Volunteer.Utilities;
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
        MissionVM missionVM = _unitOfService.Mission.GetRelatedMissions(id);
        missionVM = missionVM.Take(3);
        return PartialView("_IndexMissions", missionVM);
    }

    public void PostComment(long missionId, long userId, string comment){
        _unitOfService.Comment.PostComment(missionId, userId, comment);
    }

    public IActionResult RateMission(long missionId, long userId, byte rate){ 
        _unitOfService._MissionRating.RateMission(missionId, userId, rate);
         MissionVM missionVM = _unitOfService.Mission.UpdateMissionRating(missionId);
        return PartialView("_MissionRating", missionVM);
    }
}