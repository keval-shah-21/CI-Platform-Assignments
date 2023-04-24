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
        return obj.Select(ms => ConvertMissionSkillToVM(ms)
        ).ToList();
    }
    public void AddMissionSkill(List<string> skills, long missionId)
    {
        var missionSkills = skills.Select(s => new MissionSkill()
        {
            MissionId = missionId,
            SkillId = short.Parse(s),
            CreatedAt = DateTimeOffset.Now
        });
        _unitOfWork.MissionSkill.AddRange(missionSkills);
    }
    public void EditMissionSkill(List<string> skills, List<string> preLoadedSkills, long missionId)
    {
        preLoadedSkills.ForEach(pre =>
        {
            if (!skills.Contains(pre))
            {
                _unitOfWork.MissionSkill.RemoveMissionSkill(pre, missionId);
            }
        });
        List<string> newSkills = new();
        skills.ForEach(s =>
        {
            if (!preLoadedSkills.Contains(s))
            {
                newSkills.Add(s);
            }
        });
        if (newSkills.Any())
            AddMissionSkill(newSkills, missionId);
    }
    public static MissionSkillVM ConvertMissionSkillToVM(MissionSkill ms)
    {
        return new MissionSkillVM()
        {
            MissionId = ms.MissionId,
            MissionSkillId = ms.MissionSkillId,
            SkillId = ms.SkillId
        };
    }
}
