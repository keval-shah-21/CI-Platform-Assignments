using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System.Linq.Expressions;

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
        return obj.Select(s => ConvertSkillToVM(s)
        ).ToList();
    }
    public SkillVM GetSkillById(long id)
    {
        return ConvertSkillToVM(_unitOfWork.Skill.GetFirstOrDefault(s => s.SkillId == id));
    }
    public void SaveSkill(SkillVM s)
    {
        _unitOfWork.Skill.Add(new Skill()
        {
            SkillName = s.SkillName,
            CreatedAt = DateTimeOffset.Now,
            Status = s.Status
        });
    }
    public void UpdateSkill(SkillVM s)
    {
        Skill skill = _unitOfWork.Skill.GetFirstOrDefault(m => m.SkillId== s.SkillId);
        skill.Status = s.Status;
        skill.SkillName = s.SkillName;
        skill.UpdatedAt = DateTimeOffset.Now;
    }
    public void UpdateStatusByid(long id, int value)
    {
        Skill skill = _unitOfWork.Skill.GetFirstOrDefault(s => s.SkillId == id);
        skill.Status = value == 1;
        skill.UpdatedAt = DateTimeOffset.Now;
    }
    public void DeleteSkill(long id)
    {
        _unitOfWork.Skill.RemoveById(id);
    }

    public List<SkillVM> Search(string? query)
    {
        IEnumerable<Skill> skills = _unitOfWork.Skill.GetAll();

        return string.IsNullOrEmpty(query) ? skills.Select(t => ConvertSkillToVM(t)).ToList()
            : skills
                .Where(t => t.SkillName.ToLower().Contains(query.ToLower()))
                .Select(t => ConvertSkillToVM(t))
                .ToList();
    }
    public bool IsAlreadyUsed(long id)
    {
        return _unitOfWork.Skill.IsAlreadyUsed(id);
    }
    public bool IsSkillUnique(string skillName, long? id)
    {
        Expression<Func<Skill, bool>> filter;
        if (id != null)
            filter = s => s.SkillName == skillName && s.SkillId!= id;
        else
            filter = s => s.SkillName == skillName;
        return _unitOfWork.Skill.GetFirstOrDefault(filter) == null;
    }

    public static SkillVM ConvertSkillToVM(Skill s)
    {
        return new SkillVM()
        {
            SkillId = s.SkillId,
            SkillName = s.SkillName,
            Status = s.Status,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        };
    }
}
