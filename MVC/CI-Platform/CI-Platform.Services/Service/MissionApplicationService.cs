using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services;

public class MissionApplicationService: IMissionApplicationService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionApplicationService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionApplicationVM> GetAll()
    {
        IEnumerable<MissionApplication> obj = _unitOfWork.MissionApplication.GetAll();
        if(obj == null) return null!;
        return obj.Select(ma => new MissionApplicationVM(){
            MissionApplicationId = ma.MissionApplicationId,
            MissionId = ma.MissionId,
            UserId = ma.UserId,
            AppliedAt = ma.AppliedAt,
            ApprovalStatus = ma.ApprovalStatus
        }
        ).ToList();
    }
}
