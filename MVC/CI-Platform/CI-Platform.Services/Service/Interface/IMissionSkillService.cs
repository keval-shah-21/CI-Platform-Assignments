using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionSkillService
{
    List<MissionSkillVM> GetAll();
    void EditMissionSkill(List<string> skills, List<string> preLoadedSkills, long missionId);
    void AddMissionSkill(List<string> skills, long missionId);
}
