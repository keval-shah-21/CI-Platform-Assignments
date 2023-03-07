using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
namespace CI_Platform.Services.Service;

public class SkillService:ISkillService
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
    }
    public List<SkillVM> GetAll()
    {
        IEnumerable<Skill> obj = _unitOfWork.Skill.GetAll();
        if(obj == null) return null!;
        return obj.Select(s => new SkillVM(){
            SkillId = s.SkillId,
            SkillName = s.SkillName
        }
        ).ToList();
    }
}
