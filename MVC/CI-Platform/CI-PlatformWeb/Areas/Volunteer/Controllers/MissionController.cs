﻿using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using CI_PlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[AuthenticateAdmin]
public class MissionController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public MissionController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }

    public IActionResult MissionDetails(long? id, string? applySuccess)
    {
        if (id == 0)
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        MissionVM missionVM = _unitOfService.Mission.GetMissionById(id);
        if (missionVM == null)
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        ViewBag.ApplySuccess = applySuccess;
        return View(missionVM);
    }

    public IActionResult RelatedMissions(long id)
    {
        List<IndexMissionVM> missionVM = _unitOfService.Mission.GetRelatedMissions(id);
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
    public async Task<IActionResult> RecommendMission(long missionId, long userId, long[] toUsers)
    {
        var url = Url.Action("MissionDetails", "Mission", new { id = missionId }, "https");
        await _unitOfService.MissionInvite.RecommendMission(missionId, userId, toUsers, url);
        _unitOfService.Save();
        List<UserVM> users = _unitOfService.User.GetAllUsersToRecommendMission();
        ViewBag.UserId = userId;
        ViewBag.MissionId = missionId;

        string title = await _unitOfService.Mission.GetMissionNameById(missionId);
        UserVM user = await _unitOfService.User.GetFirstOrDefaultById(userId);
        
        SendNotificationVM sendNotificationVM = new()
        {
            Message = $"{user.FirstName} {user.LastName} - Recommended this Mission - {title}",
            SettingType = NotificationSettingType.RECOMMEND_MISSION,
            NotificationType = NotificationType.RECOMMEND,
            FromUserAvatar = user.Avatar,
            Href = $"/volunteer/mission/missiondetails/{missionId}"
        };
        await _unitOfService.Notification.SendNotificationToAllUsers(sendNotificationVM, toUsers.ToList());
        return PartialView("_RecommendToCoWorker", users?.Where(u => u.UserId != userId).ToList());
    }

    [HttpPost]
    public IActionResult ToggleFavouriteMission(long missionId, long userId, bool isFavourite)
    {
        if (isFavourite)
            _unitOfService.FavouriteMission.RemoveFromFavourite(missionId, userId);
        else
            _unitOfService.FavouriteMission.AddToFavourite(missionId, userId);
        _unitOfService.Save();
        return NoContent();
    }

    [HttpPost]
    public IActionResult ApplyMission(long missionId, long userId)
    {
        try
        {
            _unitOfService.MissionApplication.ApplyMission(missionId, userId);
            _unitOfService.Save();
            return Ok(200);
        }catch(Exception e)
        {
            Console.WriteLine("Error applying mission: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult CancelMission(long missionId, long userId)
    {
        try
        {
            _unitOfService.MissionApplication.CancelMission(missionId, userId);
            return Ok(200);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error cancelling mission: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [Authentication]
    public IActionResult MissionTimesheet(long userId)
    {
        if(userId != long.Parse(HttpContext.Session.GetString("UserId")))
        {
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        }
        List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId(userId);
        return View(mtVMs);
    }

    public IActionResult AddTimesheetGoal()
    {
        string userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });

        List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(long.Parse(userId));
        missions = missions?.Where(m => m.MissionType == MissionType.GOAL)?.ToList();

        return missions.Count() > 0 ? PartialView("_AddTimesheetGoal", new MissionTimesheetGoalVM()
        {
            MissionVM = missions
        }) : NoContent();
    }

    [HttpPost]
    public IActionResult AddTimesheetGoal(MissionTimesheetGoalVM mg)
    {
        if (ModelState.IsValid)
        {
            _unitOfService.MissionTimesheet.AddTimesheetGoal(mg);
            _unitOfService.Save();
            List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId((long)mg.UserId);
            return PartialView("_MissionTimesheetPartial", mtVMs);
        }
        return NoContent();
    }
    public IActionResult AddTimesheetHour()
    {
        string userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });

        List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(long.Parse(userId));
        missions = missions?.Where(m => m.MissionType == MissionType.TIME)?.ToList();

        return missions.Count() > 0 ? PartialView("_AddTimesheetHour", new MissionTimesheetHourVM()
        {
            MissionVM = missions
        }) : NoContent();
    }
    [HttpPost]
    public IActionResult AddTimesheetHour(MissionTimesheetHourVM mh)
    {
        if (ModelState.IsValid)
        {
            _unitOfService.MissionTimesheet.AddTimesheetHour(mh);
            _unitOfService.Save();
            List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId((long)mh.UserId);
            return PartialView("_MissionTimesheetPartial", mtVMs);
        }
        return NoContent();
    }
    public IActionResult EditTimesheetGoal(long timesheetId, long userId)
    {
        MissionTimesheetGoalVM mtg = _unitOfService.MissionTimesheet.GetTimesheetGoalById(timesheetId);
        if (mtg == null) return NoContent();

        List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(userId);
        missions = missions?.Where(m => m.MissionType == MissionType.GOAL)?.ToList();
        mtg.MissionVM = missions;
        return PartialView("_EditTimesheetGoal", mtg);
    }

    public IActionResult EditTimesheetHour(long timesheetId, long userId)
    {
        MissionTimesheetHourVM mth = _unitOfService.MissionTimesheet.GetTimesheetHourById(timesheetId);
        if (mth == null) return NoContent();

        List<MissionVM> missions = _unitOfService.MissionApplication.GetAllUserMissions(userId);
        missions = missions?.Where(m => m.MissionType == MissionType.TIME)?.ToList();
        mth.MissionVM = missions;
        return PartialView("_EditTimesheetHour", mth);
    }

    [HttpPut]
    public IActionResult EditTimesheetHour(MissionTimesheetHourVM mh)
    {
        try
        {
            _unitOfService.MissionTimesheet.EditTimesheetHour(mh);
            _unitOfService.Save();
            List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId((long)mh.UserId);
            return PartialView("_MissionTimesheetPartial", mtVMs);
        }
        catch (Exception)
        {
            return NoContent();
        }
    }
    [HttpPut]
    public IActionResult EditTimesheetGoal(MissionTimesheetGoalVM mg)
    {
        try
        {
            _unitOfService.MissionTimesheet.EditTimesheetGoal(mg);
            _unitOfService.Save();
            List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId((long)mg.UserId);
            return PartialView("_MissionTimesheetPartial", mtVMs);
        }
        catch (Exception)
        {
            return NoContent();
        }
    }

    [HttpDelete]
    public IActionResult DeleteTimesheet(long timesheetId, long userId)
    {
        try
        {
            _unitOfService.MissionTimesheet.DeleteTimesheetById(timesheetId);
            _unitOfService.Save();

            List<MissionTimesheetVM> mtVMs = _unitOfService.MissionTimesheet.GetAllByUserId(userId);
            return PartialView("_MissionTimesheetPartial", mtVMs);
        }
        catch (Exception)
        {
            return NoContent();
        }
    }
}