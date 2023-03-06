using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionMediaService: IMissionMediaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionMediaService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionMediaVM> GetAll()
    {
        IEnumerable<MissionMedia> obj = _unitOfWork.MissionMedia.GetAll();
        if(obj == null) return null!;
        return obj.Select(mm => new MissionMediaVM(){
            Default = mm.Default,
            MediaName = mm.MediaName,
            MediaPath = mm.MediaPath,
            MediaType = mm.MediaType,
            MissionId = mm.MissionId,
            MissionMediaId = mm.MissionMediaId
        }
        ).ToList();
    }
}
