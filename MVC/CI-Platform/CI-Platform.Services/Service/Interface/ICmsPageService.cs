using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface ICmsPageService
{
    void SaveCmsPage(CmsPageVM cms);
    List<CmsPageVM> GetAll();

    void UpdateCmsPage(CmsPageVM cms);
    void DeleteCmsPage(long id);
}
