using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface ICmsPageService
{
    long SaveCmsPage(CmsPageVM cms);
    List<CmsPageVM> GetAll();

    void UpdateCmsPage(CmsPageVM cms);

    void DeleteCmsPage(long id);
    void DeactivateCmsPage(long id);

    bool IsSlugUnique(string slug, long? id);
    CmsPageVM GetCmsPageById(long id);

    List<CmsPageVM> Search(string? query);
}
