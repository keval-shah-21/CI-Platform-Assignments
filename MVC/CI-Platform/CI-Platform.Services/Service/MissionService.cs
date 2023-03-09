using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.Services.Service;

public class MissionService : IMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<MissionVM> GetAll()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAll();
        if (obj == null) return null!;
        // return obj.Select(mission => new MissionVM(){
        //     MissionId = mission.MissionId,
        //     MissionCity = mission.MissionCity,
        //     MissionCountry = mission.MissionCountry,
        //     Title = mission.Title,
        //     ShortDescription = mission.ShortDescription,
        //     MissionThemeId = mission.MissionThemeId,
        //     OrganizationName = mission.OrganizationName,
        //     StartDate = mission.StartDate,
        //     EndDate = mission.EndDate,
        //     TotalSeats = mission.TotalSeats,
        //     MissionType = mission.MissionType,
        //     MissionRating = mission.MissionRating,
        //     RegistrationDeadline = mission.RegistrationDeadline
        // }).ToList();
        return null;
    }

    public List<MissionVM> GetAllIndexMission()
    {
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAllMission();
        if (obj == null) return null!;
        return obj.Select(mission =>
            new MissionVM(){
                MissionId = mission.MissionId,
                MissionCity = mission.MissionCityNavigation.CityName,
                MissionCountry = mission.MissionCountryNavigation.CountryName,
                Title = mission.Title,
                ShortDescription = mission.ShortDescription,
                MissionThemeName = mission.MissionTheme?.MissionThemeName,
                OrganizationName = mission.OrganizationName,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                TotalSeats = mission.TotalSeats,
                MissionType = mission.MissionType,
                MissionRating = mission.MissionRating,
                RegistrationDeadline = mission.RegistrationDeadline,
                MissionMedia = GetIndexMissionMedia(mission),
                FavouriteMissionVM = GetIndexMissionFavourite(mission),
                MissionApplicationVM = GetIndexMissionApplication(mission),
                MissionGoalVM = GetIndexMissionGoal(mission),
                MissionSkillVM = GetIndexMissionSkill(mission)
            }).ToList();
    }

    internal string GetIndexMissionMedia(Mission mission)
    {
        MissionMedia mm = mission.MissionMedia?.FirstOrDefault(mm => mm.Default == true)!;
        return mm != null ? mm.MediaPath + mm.MediaName + mm.MediaType : "";
    }

    internal List<FavouriteMissionVM> GetIndexMissionFavourite(Mission mission)
    {
        return mission.FavouriteMissions.LongCount() > 0 ? mission.FavouriteMissions.Select(fm =>
            new FavouriteMissionVM()
            {
                FavouriteMissionId = fm.FavouriteMissionId,
                MissionId = fm.MissionId,
                UserId = fm.UserId
            }).ToList() : null!;
    }

    internal MissionGoalVM GetIndexMissionGoal(Mission mission){
        return mission.MissionGoals.LongCount() > 0 ? new MissionGoalVM()
            {
                GoalAchieved = mission.MissionGoals.First().GoalAchieved,
                GoalObjective = mission.MissionGoals.First().GoalObjective,
                GoalValue = mission.MissionGoals.First().GoalValue,
                MissionGoalId = mission.MissionGoals.First().MissionGoalId,
                MissionId = mission.MissionGoals.First().MissionId
            } : null!;
    }

    internal List<MissionApplicationVM> GetIndexMissionApplication(Mission mission){
        return mission.MissionApplications.LongCount() > 0 ? mission.MissionApplications.Select(ma =>
            new MissionApplicationVM()
            {
                AppliedAt = ma.AppliedAt,
                ApprovalStatus = ma.ApprovalStatus,
                MissionApplicationId = ma.MissionApplicationId,
                MissionId = ma.MissionId,
                UserId = ma.UserId
            }).ToList() : null!;
    }

    internal List<MissionSkillVM> GetIndexMissionSkill(Mission mission){
        return mission.MissionSkills.LongCount() > 0 ? mission.MissionSkills.Select(ms =>
            new MissionSkillVM()
            {
                MissionId = ms.MissionId,
                MissionSkillId = ms.MissionSkillId,
                SkillId = ms.SkillId
            }).ToList() : null!;
    }
}