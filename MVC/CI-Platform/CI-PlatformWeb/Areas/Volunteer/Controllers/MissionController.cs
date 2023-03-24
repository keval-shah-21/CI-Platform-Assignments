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

    public IActionResult RelatedMissions(long id)
    {
        List<MissionVM> missionVM = _unitOfService.Mission.GetRelatedMissions(id);
        missionVM = missionVM.Take(3).ToList();
        ViewBag.TotalMissions = missionVM.Count();
        return PartialView("_IndexMissions", missionVM);
    }

    [HttpPost]
    public IActionResult PostComment(long missionId, long userId, string comment)
    {
        _unitOfService.Comment.PostComment(missionId, userId, comment);
        _unitOfService.Save();
        MissionVM mission = _unitOfService.Mission.GetMissionById(missionId);
        return PartialView("_MissionComments", mission);
    }

    [HttpPost]
    public IActionResult RateMission(long missionId, long userId, byte rate)
    {
        _unitOfService.MissionRating.RateMission(missionId, userId, rate);
        MissionVM missionVM = _unitOfService.Mission.UpdateMissionRating(missionId);
        _unitOfService.Save();
        return PartialView("_MissionRating", missionVM);
    }

    [HttpPost]
    public IActionResult RecommendMission(long missionId, long userId, long[] toUsers)
    {
        var url = Url.Action("MissionDetails", "Mission", new { id = missionId }, "https");
        _unitOfService.MissionInvite.RecommendMission(missionId, userId, toUsers, url);
        _unitOfService.Save();
        List<UserVM> users = _unitOfService.User.GetAllUsersToRecommendMission();
        ViewBag.UserId = userId;
        ViewBag.MissionId = missionId;
        return PartialView("_RecommendToCoWorker", users?.Where(u => u.UserId != userId).ToList());
    }

    [HttpPost]
    public IActionResult ToggleFavouriteMission(long missionId, long userId, bool isFavourite)
    {
        if (isFavourite)
        {
            _unitOfService.FavouriteMission.RemoveFromFavourite(missionId, userId);
        }
        else
        {
            _unitOfService.FavouriteMission.AddToFavourite(missionId, userId);
        }
        _unitOfService.Save();
        return NoContent();
    }
}