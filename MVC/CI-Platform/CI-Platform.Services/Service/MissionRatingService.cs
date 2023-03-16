using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionRatingService: IMissionRatingService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionRatingService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionRatingVM> GetAll()
    {
        IEnumerable<MissionRating> obj = _unitOfWork.MissionRating.GetAll();
        if(obj == null) return null!;
        return obj.Select(mr => ConvertMissionRatingToVM(mr)
        ).ToList();
    }

    public static MissionRatingVM ConvertMissionRatingToVM(MissionRating mr)
    {
        return new MissionRatingVM()
        {
            MissionId = mr.MissionId,
            MissionRatingId = mr.MissionRatingId,
            Rating = mr.Rating,
            UserId = mr.UserId
        };
    }
}
