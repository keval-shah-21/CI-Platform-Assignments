using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionMediaService
{
    List<MissionMediaVM> GetAll();
    void AddMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId);
    void EditMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId, List<string>? preLoaded);

}
