using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.Constants;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Services.Service;

namespace CI_Platform.Services;

public class MissionApplicationService : IMissionApplicationService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionApplicationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<MissionApplicationVM> GetAllAdmin()
    {
        IEnumerable<MissionApplication> obj = _unitOfWork.MissionApplication.GetAllAdmin();
        return obj.Select(ma => ConvertToVMAdmin(ma)
        ).ToList();
    }
    public List<MissionApplicationVM> GetAllWithInclude()
    {
        IEnumerable<MissionApplication> obj = _unitOfWork.MissionApplication.GetAllWithInclude();
        if (obj == null) return null!;
        return obj.Select(ma => ConvertMissionApplicationToVM(ma)
        ).ToList();
    }
    public MissionApplicationVM GetById(long id)
    {
        return ConvertToVMAdmin(_unitOfWork.MissionApplication.GetFirstOrDefaultWithInclude(m => m.MissionApplicationId == id));
    }
    public static MissionApplicationVM ConvertMissionApplicationToVM(MissionApplication ma)
    {
        return new MissionApplicationVM()
        {
            MissionApplicationId = ma.MissionApplicationId,
            MissionId = ma.MissionId,
            MissionName = ma.Mission.Title,
            UserId = ma.UserId,
            AppliedAt = ma.AppliedAt,
            ApprovalStatus = (ApprovalStatus)ma.ApprovalStatus,
            UserName = ma.User?.FirstName + " " + ma.User?.LastName,
            Avatar = ma.User?.Avatar,
        };
    }
    public static MissionApplicationVM ConvertToVMAdmin(MissionApplication ma)
    {
        return new MissionApplicationVM()
        {
            MissionApplicationId = ma.MissionApplicationId,
            MissionId = ma.MissionId,
            MissionName = ma.Mission.Title,
            User = ConvertUserToProfileVM(ma.User),
            UserId = ma.UserId,
            AppliedAt = ma.AppliedAt,
            ApprovalStatus = (ApprovalStatus)ma.ApprovalStatus,
            UserName = ma.User?.FirstName + " " + ma.User?.LastName,
            Avatar = ma.User?.Avatar,
        };
    }
    public static ProfileVM ConvertUserToProfileVM(User user)
    {
        return new ProfileVM()
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            WhyIVolunteer = user.WhyIVolunteer,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            Department = user.Department,
            LinkedInUrl = user.LinkedInUrl,
            EmployeeId = user.EmployeeId,
            Availability = (Availability)user.Availability,
            CityId = user.CityId,
            CityName = user.City.CityName,
            CountryName = user.Country.CountryName,
            CountryId = user.CountryId,
            UserSkillVMs = user.UserSkills.Select(us => UserSkillService.ConvertUserSkillToVM(us)).ToList(),
        };
    }
    public void ApplyMission(long missionId, long userId)
    {
        _unitOfWork.MissionApplication.Add(new MissionApplication()
        {
            AppliedAt = DateTime.Now,
            UserId = userId,
            MissionId = missionId,
            CreatedAt = DateTimeOffset.Now,
            ApprovalStatus= 0,
        });
    }
    public void UpdateStatus(long id, byte value)
    {
        _unitOfWork.MissionApplication.UpdateStatus(id, value); 
    }
    public List<MissionApplicationVM> Search(string? query)
    {
        IEnumerable<MissionApplication> apps = _unitOfWork.MissionApplication.GetAllAdmin();

        return string.IsNullOrEmpty(query) ? apps.Select(s => ConvertToVMAdmin(s)).ToList()
            : apps
                .Where(s => s.Mission.Title.ToLower().Contains(query.ToLower()) ||
                        (s.User.FirstName.ToLower() + ' ' + s.User.LastName.ToLower()).Contains(query.ToLower()))
                .Select(s => ConvertToVMAdmin(s))
                .ToList();
    }
    public void CancelMission(long missionId, long userId)
    {
        _unitOfWork.MissionApplication.CancelMission(missionId, userId);
    }
    public List<MissionVM> GetAllUserMissions(long userId)
    {
        IEnumerable<MissionApplication> miVMs = _unitOfWork.MissionApplication.GetAllForStoryMissions();
        if (miVMs == null) return null!;
        return miVMs.Where(mi => mi.UserId == userId && mi.ApprovalStatus == 1)
            .Select(mi =>
        new MissionVM()
        {
            MissionId = mi.Mission.MissionId,
            Title = mi.Mission.Title,
            MissionType = mi.Mission.MissionType ? MissionType.GOAL : MissionType.TIME,
        }).ToList();
    }
}
