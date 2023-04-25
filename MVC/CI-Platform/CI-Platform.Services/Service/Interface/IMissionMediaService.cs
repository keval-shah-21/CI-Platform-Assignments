using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionMediaService
{
    List<MissionMediaVM> GetAll();
    void AddAllMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId);
    void EditAllMissionMedia(string wwwRootPath, List<IFormFile> images, long missionId, List<string>? preLoaded);

}
