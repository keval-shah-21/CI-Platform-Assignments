using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface
{
    public interface IMissionDocumentService
    {
        List<MissionDocumentVM> GetAll();
        void AddMissionDocuments(string wwwRootPath, List<IFormFile> images, long missionId);
        void EditMissionDocuments(string wwwRootPath, List<IFormFile> images, long missionId, List<string>? preLoaded);
    }
}
