using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class MissionSkillService: IMissionSkillService
{
    private readonly IUnitOfWork _unitOfWork;

    public MissionSkillService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<MissionSkillVM> GetAll()
    {
        IEnumerable<MissionSkill> obj = _unitOfWork.MissionSkill.GetAll();
        if(obj == null) return null!;
        return obj.Select(ms => new MissionSkillVM(){
            MissionId = ms.MissionId,
            MissionSkillId = ms.MissionSkillId,
            SkillId = ms.SkillId
        }
        ).ToList();
    }
}
