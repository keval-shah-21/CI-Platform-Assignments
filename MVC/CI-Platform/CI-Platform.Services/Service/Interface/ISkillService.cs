using CI_Platform.Entities.ViewModels;
namespace CI_Platform.Services.Service.Interface;

public interface ISkillService
{
    List<SkillVM> GetAll();
    SkillVM GetSkillById(long id);
    void SaveSkill(SkillVM s);
    void UpdateSkill(SkillVM s);
    bool IsAlreadyUsed(long id);
    void UpdateStatusByid(long id, int value);
    void DeleteSkill(long id);
    List<SkillVM> Search(string? query);
    bool IsSkillUnique(string skillName, long? id);
}

