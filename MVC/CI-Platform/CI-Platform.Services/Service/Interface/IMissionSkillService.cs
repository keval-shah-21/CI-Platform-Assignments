using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionSkillService
{
    List<MissionSkillVM> GetAll();
}
