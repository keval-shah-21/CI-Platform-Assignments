using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionSkillRepository :IRepository<MissionSkill>
{
    void RemoveMissionSkill(string skillId, long missionId);
}
