using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface ICmsPageRepository:IRepository<CmsPage>
{
    void RemoveById(long cmsPageId);
}
