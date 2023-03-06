using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services;

public class FavouriteMissionService : IFavouriteMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavouriteMissionService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<FavouriteMissionVM> GetAll()
    {
        IEnumerable<FavouriteMission> obj = _unitOfWork.FavouriteMission.GetAll();
        if(obj == null) return null!;
        return obj.Select(fm => new FavouriteMissionVM(){
            FavouriteMissionId = fm.FavouriteMissionId,
            MissionId = fm.MissionId,
            UserId = fm.UserId
        }).ToList();
    }
}
