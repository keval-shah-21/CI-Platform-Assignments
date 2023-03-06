using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.Services.Service;

public class MissionService:IMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }

    public List<MissionVM> GetAll(){
        IEnumerable<Mission> obj = _unitOfWork.Mission.GetAll();
        if(obj == null) return null!;
        return obj.Select(mission => new MissionVM(){
            MissionId = mission.MissionId,
            MissionCity = mission.MissionCity,
            MissionCountry = mission.MissionCountry,
            Title = mission.Title,
            ShortDescription = mission.ShortDescription,
            MissionThemeId = mission.MissionThemeId,
            OrganizationName = mission.OrganizationName,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            TotalSeats = mission.TotalSeats,
            MissionType = mission.MissionType,
            MissionRating = mission.MissionRating,
            RegistrationDeadline = mission.RegistrationDeadline
        }).ToList();
    }
}