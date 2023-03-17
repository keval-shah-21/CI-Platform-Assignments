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
        return obj.Select(fm => ConvertFavouriteMissionToVM(fm)).ToList();
    }

    public static FavouriteMissionVM ConvertFavouriteMissionToVM(FavouriteMission favouriteMission)
    {
        return new FavouriteMissionVM()
        {
            FavouriteMissionId = favouriteMission.FavouriteMissionId,
            MissionId = favouriteMission.MissionId,
            UserId = favouriteMission.UserId
        };
    }

    public void AddToFavourite(long missionId, long userId)
    {
        FavouriteMission fmVM = new FavouriteMission()
        {
            MissionId = (long)missionId,
            UserId = (long)userId,
            CreatedAt = DateTime.UtcNow,
        };
        _unitOfWork.FavouriteMission.Add(fmVM);
    }
    public void RemoveFromFavourite(long userId)
    {
        FavouriteMission fmVM = _unitOfWork.FavouriteMission.GetFirstOrDefault(fm => fm.UserId == userId);
        _unitOfWork.FavouriteMission.Remove(fmVM);
    }
    public void Add(FavouriteMission favouriteMission)
    {
        _unitOfWork.FavouriteMission.Add(favouriteMission);
    }

    public void Remove(FavouriteMission favouriteMission)
    {
        _unitOfWork.FavouriteMission.Remove(favouriteMission);
    }
}
