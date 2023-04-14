using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface ISkillRepository:IRepository<Skill>
{
    void RemoveById(long id);
    bool IsAlreadyUsed(long id);
}
