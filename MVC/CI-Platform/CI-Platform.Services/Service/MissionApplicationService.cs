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
    public List<MissionApplicationVM> GetAll()
    {
        IEnumerable<MissionApplication> obj = _unitOfWork.MissionApplication.GetAll();
        if (obj == null) return null!;
        return obj.Select(ma => ConvertMissionApplicationToVM(ma)
        ).ToList();
    }

    public static MissionApplicationVM ConvertMissionApplicationToVM(MissionApplication ma)
    {
        return new MissionApplicationVM()
        {
            MissionApplicationId = ma.MissionApplicationId,
            MissionId = ma.MissionId,
            UserId = ma.UserId,
            AppliedAt = ma.AppliedAt,
            ApprovalStatus = ma.ApprovalStatus == 0 ? ApprovalStatus.PENDING : ma.ApprovalStatus == 1 ? ApprovalStatus.APPROVED : ApprovalStatus.DECLINED,
            UserName = ma.User?.FirstName + " " + ma.User?.LastName,
            Avatar = ma.User?.Avatar,
        };
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
